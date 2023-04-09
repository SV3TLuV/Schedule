using Microsoft.EntityFrameworkCore;

namespace Schedule.Core.Common.Interfaces;

public interface IScheduleDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}