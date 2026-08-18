using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Api.DependencyInjection;
using SecurityMonitor.Api.Endpoints.Device;
using SecurityMonitor.Api.Endpoints.Device.Groups;
using SecurityMonitor.Api.Endpoints.Device.Groups.Zone;
using SecurityMonitor.Api.Endpoints.SecurityScheme;
using SecurityMonitor.Application.Devices;
using SecurityMonitor.Application.Devices.Groups.Zones;
using SecurityMonitor.Application.SecuritySchemes;
using SecurityMonitor.Infrastructure.Persistence;
using SecurityMonitor.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);
builder.Services.AddRequestHandlers();
builder.Services.AddScoped<ISecuritySchemeRepository, SecuritySchemeRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IZoneRepository, ZoneRepository>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapSecuritySchemeEndpoints();
app.MapDeviceEndpoints();
app.MapZoneEndpoints();
app.MapGroupEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();