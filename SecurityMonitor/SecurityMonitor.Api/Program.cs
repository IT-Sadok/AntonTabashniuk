using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Api.Endpoints.SecurityScheme;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Delete;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Update;
using SecurityMonitor.Infrastructure.Persistence;
using SecurityMonitor.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<SecuritySchemeCreateHandler>();
builder.Services.AddScoped<SecuritySchemeUpdateHandler>();
builder.Services.AddScoped<SecuritySchemeGetHandler>();
builder.Services.AddScoped<SecuritySchemeGetAllHandler>();
builder.Services.AddScoped<SecuritySchemeDeleteHandler>();
builder.Services.AddScoped<ISecuritySchemeRepository, SecuritySchemeRepository>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapSecuritySchemeEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();