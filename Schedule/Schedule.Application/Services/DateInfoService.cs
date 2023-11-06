using System.Globalization;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;
using WeekType = Schedule.Core.Common.Enums.WeekType;

namespace Schedule.Application.Services;

public sealed class DateInfoService : IDateInfoService
{
    private readonly Calendar _calendar;
    private readonly DayOfWeek _firstDayOfWeek;

    public DateInfoService()
    {
        var timeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        _calendar = timeFormatInfo.Calendar;
        _firstDayOfWeek = DayOfWeek.Monday;
    }

    public Date GetDate(DateTime dateTime)
    {
        return new Date
        {
            Value = dateTime.Date,
            Term = GetTerm(dateTime),
            DayId = GetDayId(dateTime),
            WeekTypeId = (int)GetWeekType(dateTime)
        };
    }

    public int CurrentTerm => GetTerm(CurrentDateTime);
    public DateTime CurrentDateTime => DateTime.Now;
    public Date CurrentDate => GetDate(CurrentDateTime);

    public int CurrentWeekOfYear => GetWeekOfYear(CurrentDateTime);
    public WeekType CurrentWeekType => GetWeekType(CurrentDateTime);
    public int MaxWeekOfYear => GetWeekOfYear(new DateTime(CurrentDateTime.Year, 12, 31));

    public int CurrentDayId => GetDayId(CurrentDateTime);

    public Date GetNextDate(DateTime dateTime)
    {
        return GetDate(dateTime.Date.AddDays(1));
    }

    public int GetTerm(DateTime dateTime)
    {
        return dateTime.Month < 9 ? 2 : 1;
    }

    public int GetGroupTerm(int enrollmentYear, bool isAfterEleven)
    {
        int term;
        
        if (CurrentTerm == 2)
        {
            term = CurrentTerm + 2 * (CurrentDateTime.Year - enrollmentYear - 1);
        }
        else
        {
            term = CurrentTerm + 2 * (CurrentDateTime.Year - enrollmentYear);
        }

        return isAfterEleven ? term + 2 : term;
    }

    public int GetWeekOfYear(DateTime dateTime)
    {
        return _calendar.GetWeekOfYear(dateTime,
            CalendarWeekRule.FirstFourDayWeek,
            _firstDayOfWeek);
    }

    public WeekType GetWeekType(DateTime dateTime)
    {
        var yellowWeekDate = dateTime.Month < 9
            ? new DateTime(dateTime.Year, 1, 12)
            : new DateTime(dateTime.Year, 9, 1);
        var yellowWeekIsOdd = IsOddWeek(yellowWeekDate);
        var isOdWeek = IsOddWeek(dateTime);
        return yellowWeekIsOdd == isOdWeek ? WeekType.Yellow : WeekType.Green;
    }

    public int GetDayId(DateTime dateTime)
    {
        var day = (int)dateTime.DayOfWeek;
        return day == 0 ? 7 : day;
    }

    private bool IsOddWeek(DateTime dateTime)
    {
        var week = GetWeekOfYear(dateTime);
        return (week & 1) != 0;
    }
}