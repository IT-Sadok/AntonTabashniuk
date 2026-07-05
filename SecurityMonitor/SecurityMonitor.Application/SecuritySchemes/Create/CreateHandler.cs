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
        if (await repository.ExistsAsync(command.Name, cancellationToken))
        {
            return Result<CreateResponse>.Failure("Security scheme already exists.");
        }

        var securityScheme = new SecurityScheme(command.Name, command.Description);

        var id = await repository.AddAsync(securityScheme, cancellationToken);
        
        return Result<CreateResponse>.Success(new CreateResponse(id));
    }
}
