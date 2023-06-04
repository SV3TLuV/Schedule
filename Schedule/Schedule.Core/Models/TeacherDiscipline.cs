namespace Schedule.Core.Models;

public class TeacherDiscipline
{
    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; } = null!;

    public int DisciplineId { get; set; }

    public Discipline Discipline { get; set; } = null!;
}