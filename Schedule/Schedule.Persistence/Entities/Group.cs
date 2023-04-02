using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class Group
{
    public int GroupId { get; set; }

    public string Name { get; set; } = null!;

    public int SpecialityCodeId { get; set; }

    public int Course { get; set; }

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public virtual SpecialityCode SpecialityCode { get; set; } = null!;

    public virtual ICollection<Template> Templates { get; } = new List<Template>();

    public virtual ICollection<Timetable> Timetables { get; } = new List<Timetable>();

    public virtual ICollection<Discipline> Disciplines { get; } = new List<Discipline>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
