using SecurityMonitor.Api.Endpoints.Device.Mappers;
using SecurityMonitor.Api.Endpoints.Device.Requests;
using SecurityMonitor.Application.Devices.Update;

namespace SecurityMonitor.Api.Endpoints.Device;

public static class DeviceEndpoints
{
    public static IEndpointRouteBuilder MapDeviceEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(DeviceRoutes.Base);

        group.MapPost(DeviceRoutes.Update, Update);

        return endpoints;
    }

    private static async Task<IResult> Update(DeviceUpdateRequest request, DeviceUpdateHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return Results.BadRequest(result.Error);
    }
}
