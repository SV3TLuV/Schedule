namespace Schedule.Application.ViewModels;

public sealed class TeacherClassroomViewModel
{
    public TeacherViewModel Teacher { get; set; } = null!;
    public ClassroomViewModel Classroom { get; set; } = null!;
}