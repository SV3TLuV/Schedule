using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TemplateTeacherClassroomViewModel : IMapWith<LessonTemplateTeacherClassroom>
{
    public required TeacherViewModel Teacher { get; set; }
    
    public ClassroomViewModel? Classroom { get; set; }
}