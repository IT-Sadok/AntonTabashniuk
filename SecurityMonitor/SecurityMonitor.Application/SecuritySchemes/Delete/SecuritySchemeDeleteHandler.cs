using SecurityMonitor.Application.Common;

namespace SecurityMonitor.Application.SecuritySchemes.Delete;

public class SecuritySchemeDeleteHandler
{
    private readonly ISecuritySchemeRepository repository;

    public SecuritySchemeDeleteHandler(ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<bool>> Handle(
        SecuritySchemeDeleteCommand command,
        CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            return Result<bool>.Failure("Security scheme does not exists.");
        }

        var isDeleted = await repository.DeleteAsync(command.Id, cancellationToken);
        
        if (isDeleted)
        {
            return Result<bool>.Success(isDeleted);
        }

        return Result<bool>.Failure("There was some problems when deleting security scheme");
    }
}