using Schedule.Core.Common.Enums;

namespace Schedule.Core.Common.Interfaces;

public interface IWeekInfoService
{
    int CurrentWeekOfYear { get; }

    int MaxWeekOfYear { get; }

    WeekType CurrentWeekType { get; }

    int GetWeekOfYear(DateTime dateTime);

    WeekType GetWeekType(DateTime dateTime);
}