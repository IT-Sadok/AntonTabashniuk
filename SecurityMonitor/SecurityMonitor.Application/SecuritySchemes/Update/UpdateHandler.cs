using SecurityMonitor.Application.Common;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.Update;

public sealed class UpdateHandler
{
    private readonly ISecuritySchemeRepository repository;

    public UpdateHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<UpdateResponse>> Handle(
        UpdateCommand command,
        CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            return Result<UpdateResponse>.Failure("Security scheme does not exists.");
        }
        var securityScheme = new SecurityScheme(command.Id, command.Name, command.Description);

        var isUpdated = await repository.UpdateAsync(securityScheme, cancellationToken);

        if (isUpdated)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    securityScheme.Id,
                    securityScheme.Name,
                    securityScheme.Description
                    ));
        }

        return Result<UpdateResponse>.Failure("Failed to update security scheme.");
    }
}
