using System.Globalization;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Services;

public sealed class WeekTypeService : IWeekTypeService
{
    public int GetCurrentWeekTypeId()
    {
        var cultureInfo = new CultureInfo("ru-RU");
        var weekNumberOfYear = cultureInfo.Calendar.GetWeekOfYear(DateTime.Now,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday);
        return Convert.ToInt32((weekNumberOfYear & 1) == 0) + 1;
    }

    public int GetAnotherWeekTypeId(int id)
    {
        return id == 1 ? 2 : 1;
    }
}