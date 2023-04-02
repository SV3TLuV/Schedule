using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class Discipline
{
    public int DisciplineId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Lesson> Lessons { get; } = new List<Lesson>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<SpecialityCode> SpecialityCodes { get; } = new List<SpecialityCode>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
