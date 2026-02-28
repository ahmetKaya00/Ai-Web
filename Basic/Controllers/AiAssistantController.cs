using Basic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers;

[Authorize]
public sealed class AiAssistantController : Controller
{
    private readonly GeminiChatService _gemini;

    public AiAssistantController(GeminiChatService gemini)
    {
        _gemini = gemini;
    }
    [HttpGet]
    public IActionResult Index() => View();

    public sealed class AskRequest
    {
        public string Message { get; set; } = "";
    }
    [HttpPost]
    public async Task<IActionResult> Ask([FromBody] AskRequest req, CancellationToken ct)
    {
        if (req == null || string.IsNullOrWhiteSpace(req.Message))
            return BadRequest(new { error = "message boş olamaz" });

        var prompt = $"""
            Sen BTK Akademinin asistanısın kısa, net cevaplar verirsin.
            Türkçe cevap ver.
            Önce 3 madde, sonra kısa bir örnek ver.
            yazılım dışı sorularda, burası yazılım evreni farklı soru sorma de.

            Soru:{req.Message}
        """;
        try
        {
            var answer = await _gemini.AskAsync(prompt, ct);
            return Ok(new { answer });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = ex.Message,
                inner = ex.InnerException?.Message
            });
        }
    }
}