namespace SecurityMonitor.Infrastructure.Persistence.Entities.Common;

public sealed record PagedEntityResult<TEntity>(
    IReadOnlyList<TEntity> Items,
    int TotalCount);
