﻿namespace Schedule.Core.Models;

public class Timetable
{
    public int TimetableId { get; set; }

    public int GroupId { get; set; }

    public int DateId { get; set; }

    public virtual Date Date { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}