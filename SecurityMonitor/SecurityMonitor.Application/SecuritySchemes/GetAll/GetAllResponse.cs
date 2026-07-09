using SecurityMonitor.Application.SecuritySchemes.Get;

namespace SecurityMonitor.Application.SecuritySchemes.GetAll;

public sealed record GetAllResponse(List<GetResponse>? responce);
