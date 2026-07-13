using SecurityMonitor.Application.SecuritySchemes.Create;
using SecurityMonitor.Application.SecuritySchemes.Get;
using SecurityMonitor.Application.SecuritySchemes.Update;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.Mappers;

public static class SecuritySchemeMapper
{
    public static SecuritySchemeGetResponse ToGetResponse(this SecurityScheme securityScheme)
    {
        return new SecuritySchemeGetResponse(
            securityScheme.Id,
            securityScheme.Name,
            securityScheme.Description);
    }
    public static SecuritySchemeUpdateResponse ToUpdateResponse(this SecurityScheme securityScheme)
    {
        return new SecuritySchemeUpdateResponse(
            securityScheme.Id,
            securityScheme.Name,
            securityScheme.Description);
    }

    public static SecurityScheme ToSecurityScheme(this SecuritySchemeCreateCommand createCommand)
    {
        return new SecurityScheme(
            0,
            createCommand.Name,
            createCommand.Description);
    }
}   