using System.Text.Json.Serialization;
using WebApi.Helpers;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IArticleService, ArticleService>();

    services.AddControllersWithViews();

    services.AddDistributedMemoryCache();

    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseRouting();
    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "login",
            pattern: "login",
            defaults: new { controller = "Auth", action = "Login" });

        endpoints.MapControllerRoute(
            name: "register",
            pattern: "register",
            defaults: new { controller = "Auth", action = "Register" });

        endpoints.MapControllerRoute(
            name: "articleCreate",
            pattern: "articleCreate",
            defaults: new { controller = "Article", action = "Create" });

        endpoints.MapControllerRoute(
            name: "articleNew",
            pattern: "articleNew",
            defaults: new { controller = "Article", action = "New" });

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Main}/{action=Index}/{id?}");

    });

    app.UseStaticFiles();

    app.UseSession();
    app.UseCookiePolicy();

}


app.Run("http://localhost:4000");