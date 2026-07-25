using SecurityMonitor.Api.Endpoints.Device.Mappers;
using SecurityMonitor.Api.Endpoints.SecurityScheme.Requests;
using SecurityMonitor.Application.SecuritySchemes.Update;

namespace SecurityMonitor.Api.Endpoints.SecurityScheme.Mappers;

public static class SecuritySchemeMappings
{
    public static SecuritySchemeUpdateCommand ToCommand(
        this SecuritySchemeUpdateRequest request,
        int id)
    {
        return new SecuritySchemeUpdateCommand(
            id,
            request.Name,
            request.Description,
            request.Device.ToCommand());
    }
}