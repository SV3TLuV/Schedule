using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommand : IRequest<Unit>, IMapWith<Lesson>
{
    public int LessonId { get; set; }
    public int DisciplineId { get; set; }
    public int Number { get; set; }
    public int? Subgroup { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public ICollection<TeacherClassroomIdPairViewModel> LessonTeacherClassrooms { get; set; } =
        Array.Empty<TeacherClassroomIdPairViewModel>();
}