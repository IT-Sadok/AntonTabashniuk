using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Api.Endpoints.Auth;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Infrastructure.Persistence;
using SecurityMonitor.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<CreateHandler>();
builder.Services.AddScoped<ISecuritySchemeRepository, SecuritySchemeRepository>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapSecuritySchemeEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();