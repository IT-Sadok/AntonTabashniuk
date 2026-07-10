using SecurityMonitor.Application.SecuritySchemes.Get;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;

public sealed record SecuritySchemeGetAllResponse(List<SecuritySchemeGetResponse>? responce);
