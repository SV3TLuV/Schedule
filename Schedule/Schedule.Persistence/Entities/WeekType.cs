using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class WeekType
{
    public int WeekTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Date> Dates { get; } = new List<Date>();

    public virtual ICollection<Template> Templates { get; } = new List<Template>();
}
