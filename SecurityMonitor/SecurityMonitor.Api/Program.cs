using SecurityMonitor.Api.Endpoints.Auth;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Application.SecuritySchemes.Create;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<CreateHandler>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapSecuritySchemeEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();