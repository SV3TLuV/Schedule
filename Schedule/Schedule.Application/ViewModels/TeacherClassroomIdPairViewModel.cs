using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class TeacherClassroomIdPairViewModel : IMapWith<LessonTeacherClassroom>, IMapWith<LessonChangeTeacherClassroom>
{
    public int TeacherId { get; set; }
    public int ClassroomId { get; set; }
}