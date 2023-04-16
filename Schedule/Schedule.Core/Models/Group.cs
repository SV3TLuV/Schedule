namespace Schedule.Core.Models;

public class Group
{
    public int GroupId { get; set; }

    public string Number { get; set; } = null!;

    public int SpecialityCodeId { get; set; }

    public int CourseId { get; set; }

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual SpecialityCode SpecialityCode { get; set; } = null!;

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public virtual ICollection<Group> GroupId2s { get; set; } = new List<Group>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}