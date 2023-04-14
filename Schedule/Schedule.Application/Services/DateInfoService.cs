using System.Globalization;
using Schedule.Core.Common.Interfaces;
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

    #region Properties

    public int CurrentTerm => GetTerm(DateTime.Now);
    public Date CurrentDate => GetDate(DateTime.Now);
    
    public int CurrentWeekOfYear => GetWeekOfYear(DateTime.Now);
    public WeekType CurrentWeekType => GetWeekType(DateTime.Now);
    public int MaxWeekOfYear => GetWeekOfYear(new DateTime(DateTime.Now.Year, 12, 31));

    public int CurrentDayId => GetDayId(DateTime.Now);

    #endregion

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

    public Date GetNextDate(DateTime dateTime)
    {
        return GetDate(dateTime.Date.AddDays(1));
    }

    public int GetTerm(DateTime dateTime)
    {
        return dateTime.Month < 9 ? 1 : 2;
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