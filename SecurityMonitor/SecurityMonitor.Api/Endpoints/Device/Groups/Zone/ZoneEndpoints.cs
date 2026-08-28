using Microsoft.AspNetCore.Mvc;
using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Mappers;
using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;
using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Application.Devices.Groups.Zones.Delete;
using SecurityMonitor.Application.Devices.Groups.Zones.Update;

namespace SecurityMonitor.Api.Endpoints.Device.Groups.Zone;

public static class ZoneEndpoints
{
    public static IEndpointRouteBuilder MapZoneEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(ZoneRoutes.Base, Create);

        endpoints.MapPut(ZoneRoutes.Base, Update);

        endpoints.MapDelete(ZoneRoutes.Base, Delete);

        return endpoints;
    }
    private static async Task<IResult> Create([FromBody] CreateZonesRequest request, IRequestHandler<CreateZoneCommand, Result<CreateZonesResponse>> handler, CancellationToken cancellationToken)
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

    private static async Task<IResult> Update([FromBody] UpdateZonesRequest request, IRequestHandler<UpdateZoneCommand, Result<bool>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete([FromBody] DeleteZonesRequest request, IRequestHandler<DeleteZoneCommand, Result<bool>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
