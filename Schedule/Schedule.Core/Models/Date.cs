﻿namespace Schedule.Core.Models;

public class Date
{
    public int DateId { get; set; }

    public DateTime Value { get; set; }

    public int Term { get; set; }

    public int DayId { get; set; }

    public int WeekTypeId { get; set; }

    public bool IsStudy { get; set; }

    public virtual Day Day { get; set; } = null!;

    public virtual WeekType WeekType { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}