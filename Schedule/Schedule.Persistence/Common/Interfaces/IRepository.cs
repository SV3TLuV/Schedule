using Schedule.Core.Common.Interfaces;

namespace Schedule.Persistence.Common.Interfaces;

public interface IRepository
{
    public void UseContext(IScheduleDbContext context);
}