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

    public virtual ICollection<Template> Templates { get; } = new List<Template>();

    public virtual ICollection<Timetable> Timetables { get; } = new List<Timetable>();

    public virtual ICollection<Group> Groups2 { get; } = new List<Group>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}