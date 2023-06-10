using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Initializers;

public sealed class DatabaseInitializer : IDbInitializer
{
    private readonly IScheduleDbContext _context;

    public DatabaseInitializer(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.MigrateAsync(cancellationToken);
    }
}