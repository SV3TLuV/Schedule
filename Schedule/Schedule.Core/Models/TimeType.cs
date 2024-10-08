﻿namespace Schedule.Core.Models;

public class TimeType
{
    public int TimeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();
}