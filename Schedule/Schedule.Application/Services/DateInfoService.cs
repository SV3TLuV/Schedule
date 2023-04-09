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
    
    public int CurrentTerm => GetTerm(DateTime.Now);
    public Date CurrentDate => GetDate(DateTime.Now);
    
    public int CurrentWeekOfYear => GetWeekOfYear(DateTime.Now);
    public WeekType CurrentWeekType => GetWeekType(DateTime.Now);
    public int MaxWeekOfYear => GetWeekOfYear(new DateTime(DateTime.Now.Year, 12, 31));

    public int CurrentDayId => GetDayId(DateTime.Now);

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
        return (GetWeekOfYear(dateTime) & 1) != 0
            ? WeekType.Green
            : WeekType.Yellow;
    }

    public int GetDayId(DateTime dateTime)
    {
        var day = (int)dateTime.DayOfWeek;
        return day == 0 ? 7 : day;
    }
}