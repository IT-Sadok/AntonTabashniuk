using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Delete;
using SecurityMonitor.Application.Devices.Groups.Zones.Update;
using SecurityMonitor.Application.Devices.Update;
using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Delete;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.GetAll;
using SecurityMonitor.Application.SecuritySchemes.Update;

namespace SecurityMonitor.Api.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddSecuritySchemeHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<
            IRequestHandler<
                SecuritySchemeCreateCommand,
                Result<SecuritySchemeCreateResponse>>,
            SecuritySchemeCreateHandler>();

        services.AddScoped<
            IRequestHandler<
                SecuritySchemeUpdateCommand,
                Result<SecuritySchemeUpdateResponse>>,
            SecuritySchemeUpdateHandler>();

        services.AddScoped<
            IRequestHandler<
                SecuritySchemeDeleteCommand,
                Result<bool>>,
            SecuritySchemeDeleteHandler>();

        services.AddScoped<
            IRequestHandler<
                SecuritySchemeGetQuery,
                Result<SecuritySchemeGetResponse>>,
            SecuritySchemeGetHandler>();

        services.AddScoped<
            IRequestHandler<
                SecuritySchemeGetAllQuery,
                Result<PagedResult<SecuritySchemeGetResponse>>>,
            SecuritySchemeGetAllHandler>();

        return services;
    }

    public static IServiceCollection AddDeviceHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<
            IRequestHandler<
                DeviceUpdateCommand,
                Result<bool>>,
            DeviceUpdateHandler>();

        return services;
    }
    public static IServiceCollection AddZoneHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<
            IRequestHandler<
                UpdateZoneCommand,
                Result<bool>>,
            UpdateZoneHandler>();

        services.AddScoped<
            IRequestHandler<
                CreateZoneCommand,
                Result<CreateZonesResponse>>,
            CreateZoneHandler>();
        
        services.AddScoped<
            IRequestHandler<
                DeleteZoneCommand,
                Result<bool>>,
            DeleteZoneHandler>();

        return services;
    }
}
