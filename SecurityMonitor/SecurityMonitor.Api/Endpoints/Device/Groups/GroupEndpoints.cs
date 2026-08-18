using Microsoft.AspNetCore.Mvc;
using SecurityMonitor.Api.Endpoints.Device.Groups.Mappers;
using SecurityMonitor.Api.Endpoints.Device.Groups.Requests;
using SecurityMonitor.Application.Common;
using SecurityMonitor.Application.Devices.Groups.Create;
using SecurityMonitor.Application.Devices.Groups.Delete;
using SecurityMonitor.Application.Devices.Groups.Update;

namespace SecurityMonitor.Api.Endpoints.Device.Groups;

public static class GroupEndpoints
{
    public static IEndpointRouteBuilder MapGroupEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(GroupRoutes.Base, Create);

        endpoints.MapPut(GroupRoutes.Base, Update);

        endpoints.MapDelete(GroupRoutes.Base, Delete);

        return endpoints;
    }
    private static async Task<IResult> Create([FromBody] CreateGroupsRequest request, IRequestHandler<CreateGroupsCommand, Result<CreateGroupsResponse>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Created(
            $"{GroupRoutes.Base}/{result.Value}",
            result.Value);
    }

    private static async Task<IResult> Update([FromBody] UpdateGroupsRequest request, IRequestHandler<UpdateGroupsCommand, Result<UpdateGroupsResponse>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Delete([FromBody] DeleteGroupsRequest request, IRequestHandler<DeleteGroupsCommand, Result<bool>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
