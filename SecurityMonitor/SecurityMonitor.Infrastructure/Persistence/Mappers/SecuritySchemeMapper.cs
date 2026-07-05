using SecurityMonitor.Domain.Administrative;
using SecurityMonitor.Infrastructure.Persistence.Entities;

namespace SecurityMonitor.Infrastructure.Persistence.Mappers;

public static class SecuritySchemeMapper
{
    public static SecuritySchemeEntity ToEntity(this SecurityScheme model)
    {
        return new SecuritySchemeEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
    }

    public static SecurityScheme ToDomain(this SecuritySchemeEntity entity)
    {
        return new SecurityScheme(
            entity.Name,
            entity.Description)
        {
            Id = entity.Id
        };
    }
}
