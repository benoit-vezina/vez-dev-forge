using Microsoft.EntityFrameworkCore;
using PerfectRestApi.Repository;
using PerfectRestApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Add Newtonsoft JSON support for JsonPatch
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICookieService, CookieService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
