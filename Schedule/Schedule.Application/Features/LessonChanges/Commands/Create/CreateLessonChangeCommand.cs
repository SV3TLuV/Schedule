using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonChanges.Commands.Create;

public sealed class CreateLessonChangeCommand : IRequest<int>, IMapWith<LessonChange>
{
    public int Number { get; set; }
    public int LessonId { get; set; }
    public DateOnly Date { get; set; }
    public int? Subgroup { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int DisciplineId { get; set; }
    public ICollection<TeacherClassroomIdPairViewModel> TeacherClassroomIdPair { get; set; } =
        Array.Empty<TeacherClassroomIdPairViewModel>();
}