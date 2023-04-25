namespace Schedule.Core.Models;

public class Group
{
    public int GroupId { get; set; }

    public string Number { get; set; } = null!;

    public int SpecialityId { get; set; }

    public int CourseId { get; set; }

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public virtual ICollection<GroupGroup> GroupGroups1 { get; set; } = new List<GroupGroup>();

    public virtual ICollection<GroupGroup> GroupGroups { get; set; } = new List<GroupGroup>();

    public virtual ICollection<TeacherGroup> TeacherGroups { get; set; } = new List<TeacherGroup>();
}