using Microsoft.EntityFrameworkCore;
using EmpManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Use SQLite for the free-tier live demo (no separate DB server needed),
// or SQL Server for local development / production — controlled by the
// "UseSqlite" setting in appsettings.json (see appsettings.Production.json).
var useSqlite = builder.Configuration.GetValue<bool>("UseSqlite");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (useSqlite)
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

var app = builder.Build();

// Ensure the database schema exists on startup.
// Important for the SQLite demo: the free hosting file system resets on
// restart, so the database is freshly (re)created each time the app boots.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();

// Exposed so the test project can reference the entry point (needed for
// WebApplicationFactory-based integration tests).
public partial class Program { }