using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class LessonTeacherClassroomViewModel : IMapWith<LessonTeacherClassroom>
{
    public LessonViewModel Lesson { get; set; } = null!;

    public TeacherViewModel Teacher { get; set; } = null!;
    
    public ClassroomViewModel? Classroom { get; set; }
}