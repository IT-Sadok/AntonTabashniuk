using SecurityMonitor.Api.Endpoints.Zone.Mappers;
using SecurityMonitor.Api.Endpoints.Zone.Models;
using SecurityMonitor.Api.Endpoints.Zone.Requests;
using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Delete;
using SecurityMonitor.Application.Devices.Groups.Zones.Update;

namespace SecurityMonitor.Api.Endpoints.Zone;

public static class ZoneEndpoints
{
    public static IEndpointRouteBuilder MapSecuritySchemeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(ZoneRoutes.Base, Create);

        endpoints.MapPut(ZoneRoutes.Base, Update);

        endpoints.MapDelete(ZoneRoutes.Base, Delete);

        return endpoints;
    }
    private static async Task<IResult> Create(CreateZonesRequest request, CreateZoneHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Created(
            $"{ZoneRoutes.Base}/{result.Value}",
            result.Value);
    }

    private static async Task<IResult> Update(UpdateZonesRequest request, UpdateZoneHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete(DeleteZonesRequest request, DeleteZoneHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
