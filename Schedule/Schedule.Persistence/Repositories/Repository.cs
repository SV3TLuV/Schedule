using Schedule.Core.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public abstract class Repository(IScheduleDbContext context)
{
    protected IScheduleDbContext Context { get; private set; } = context;

    public void UseContext(IScheduleDbContext context) => Context = context;
}