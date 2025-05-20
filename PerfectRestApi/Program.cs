using Microsoft.EntityFrameworkCore;
using PerfectRestApi.Repository;
using PerfectRestApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Add Newtonsoft JSON support for JsonPatch
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "Hello, world!");

app.Run();
