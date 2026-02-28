using System.Text;
using System.Text.Json;

namespace Basic.Services;

public sealed class GeminiChatService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _cfg;

    public GeminiChatService(HttpClient http, IConfiguration cfg)
    {
        _http = http;
        _cfg = cfg;
    }

    public async Task<string> AskAsync(string userMessage, CancellationToken ct = default)
    {
        var apiKeyRaw = (_cfg["Gemini:ApiKey"] ?? "").Trim();
        if (string.IsNullOrWhiteSpace(apiKeyRaw))
            throw new InvalidOperationException("Gemini API Key yok. User-secrets içine Gemini:ApiKey ekle.");

        var apiKey = Uri.EscapeDataString(apiKeyRaw);

        // ✅ ListModels çıktısındaki name'i birebir kullan
        var modelName = "models/gemini-2.5-flash";

        var url = new Uri(
            $"https://generativelanguage.googleapis.com/v1beta/{modelName}:generateContent?key={apiKey}"
        );

        var payload = new
        {
            contents = new[]
            {
                new {
                    role = "user",
                    parts = new[] { new { text = userMessage } }
                }
            },
            generationConfig = new
            {
                temperature = 0.6,
                maxOutputTokens = 800
            }
        };

        var json = JsonSerializer.Serialize(payload);

        using var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        using var res = await _http.SendAsync(req, ct);
        var body = await res.Content.ReadAsStringAsync(ct);

        if (!res.IsSuccessStatusCode)
            throw new InvalidOperationException($"Gemini hata: {(int)res.StatusCode} - {body}");

        using var doc = JsonDocument.Parse(body);

        if (!doc.RootElement.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
            throw new InvalidOperationException("Gemini candidates boş döndü: " + body);

        var cand0 = candidates[0];

        if (!cand0.TryGetProperty("content", out var content) ||
            !content.TryGetProperty("parts", out var parts) ||
            parts.GetArrayLength() == 0)
            throw new InvalidOperationException("Gemini content/parts boş döndü: " + body);

        var text = parts[0].GetProperty("text").GetString();
        return text ?? "";
    }
}