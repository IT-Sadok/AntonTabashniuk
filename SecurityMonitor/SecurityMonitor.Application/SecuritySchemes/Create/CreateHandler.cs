using SecurityMonitor.Application.Common;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.Create;

public sealed class CreateHandler
{
    private readonly ISecuritySchemeRepository repository;

    public CreateHandler(
        ISecuritySchemeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<CreateResponse>> Handle(
        CreateCommand command, 
        CancellationToken cancellationToken)
    {
        if (await repository.ExistsAsync(command.id, cancellationToken))
        {
            return Result<CreateResponse>.Failure("Security scheme already exists.");
        }

        var securityScheme = new SecurityScheme(command.id, command.Name, command.Description);

        var id = await repository.AddAsync(securityScheme, cancellationToken);

        if (id > 0)
        {
            return Result<CreateResponse>.Success(new CreateResponse(id));
        }

        return Result<CreateResponse>.Failure("Failed to create security scheme.");
    }
}
