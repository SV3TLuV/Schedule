﻿namespace Schedule.Core.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string Number { get; set; } = null!;

    public int SpecialityId { get; set; }

    public int TermId { get; set; }

    public int EnrollmentYear { get; set; }

    public bool IsAfterEleven { get; set; }
    
    public bool IsDeleted { get; set; }

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual Term Term { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public virtual ICollection<GroupTransfer> GroupTransfers { get; set; } =
        new List<GroupTransfer>();

    public virtual ICollection<GroupGroup> GroupGroups1 { get; set; } =
        new List<GroupGroup>();

    public virtual ICollection<GroupGroup> GroupGroups { get; set; } =
        new List<GroupGroup>();
}

public partial class Group
{
    public string Name => Speciality is not null
        ? $"{Speciality.Name}-{Number}"
        : "";
}