using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Services;


public sealed class DayService : IDayService
{
    public int GetPreviousDayId(int id)
    {
        return id == 1 ? 7 : id - 1;
    }

    public int GetCurrentDayId()
    {
        var id = (int)DateTime.Now.DayOfWeek;
        return id == 0 ? 7 : id;
    }

    public int GetNextDayId(int id)
    {
        return id == 7 ? 1 : id + 1;
    }
}