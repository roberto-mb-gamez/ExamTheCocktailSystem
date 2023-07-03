using TheCocktail.Web.Extensions;
using TheCocktail.ExternalServices;
using TheCocktail.Web.Middleware;
using TheCocktail.Application;
using TheCocktail.Persistence.SQLite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureExternalServicesRegistration();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureWebServicesRegistration(builder.Configuration);

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetService<CocktailDbContext>();
await db.Database.MigrateAsync();

app.UseGlobalExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cocktails}/{action=Index}/{id?}");

app.Run();
