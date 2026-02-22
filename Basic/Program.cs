using Basic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
{
   var config = builder.Configuration;
   var connectionString = config.GetConnectionString("database"); 
   options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
