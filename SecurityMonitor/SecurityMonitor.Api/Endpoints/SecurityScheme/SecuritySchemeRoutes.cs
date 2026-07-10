namespace SecurityMonitor.Api.Endpoints.SecurityScheme;

public static class SecuritySchemeRoutes
{
    public const string Base = "/security-schemes";

    public const string Create= "/";

    public const string Get = "/{id:int}";

    public const string GetAll = "/";

    public const string Delete = "/{id:int}";

    public const string Update = "/{id:int}";

}
