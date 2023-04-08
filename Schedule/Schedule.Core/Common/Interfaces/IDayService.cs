namespace Schedule.Core.Common.Interfaces;

public interface IDayService
{
    int GetPreviousDayId(int id);
    int GetCurrentDayId();
    int GetNextDayId(int id);
}