using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class SpecialityCode
{
    public int SpecialityCodeId { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Discipline> Disciplines { get; } = new List<Discipline>();
}
