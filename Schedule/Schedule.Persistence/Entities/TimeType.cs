using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class TimeType
{
    public int TimeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Date> Dates { get; } = new List<Date>();

    public virtual ICollection<Time> Times { get; } = new List<Time>();
}
