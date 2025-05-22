using Microsoft.EntityFrameworkCore;
using PerfectRestApi.Repository;
using PerfectRestApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Add Newtonsoft JSON support for JsonPatch
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<ICookieService, CookieService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "Hello, world!");
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = new { message = "Internal server error." };
        await context.Response.WriteAsJsonAsync(error);
    });
});

app.Run();
