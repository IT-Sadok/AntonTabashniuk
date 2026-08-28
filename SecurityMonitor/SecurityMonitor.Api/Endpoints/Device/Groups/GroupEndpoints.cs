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
        endpoints.MapPost(GroupRoutes.Base, CreateAsync);

        endpoints.MapPut(GroupRoutes.Base, UpdateAsync);

        endpoints.MapDelete(GroupRoutes.Base, DeleteAsync);

        return endpoints;
    }
    private static async Task<IResult> CreateAsync([FromBody] CreateGroupsRequest request, IRequestHandler<CreateGroupsCommand, Result<CreateGroupsResponse>> handler, CancellationToken cancellationToken)
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

    private static async Task<IResult> UpdateAsync([FromBody] UpdateGroupsRequest request, IRequestHandler<UpdateGroupsCommand, Result<UpdateGroupsResponse>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> DeleteAsync([FromBody] DeleteGroupsRequest request, IRequestHandler<DeleteGroupsCommand, Result<bool>> handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    }
}
