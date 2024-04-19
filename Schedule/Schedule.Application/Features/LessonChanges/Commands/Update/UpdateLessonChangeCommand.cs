using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonChanges.Commands.Update;

public sealed class UpdateLessonChangeCommand : IRequest<Unit>, IMapWith<LessonChange>
{
    public int LessonChangeId { get; set; }
    public int Number { get; set; }
    public int LessonId { get; set; }
    public DateOnly Date { get; set; }
    public int? Subgroup { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public int DisciplineId { get; set; }
    public TeacherClassroomIdPairViewModel? TeacherClassroomIdPair { get; set; }
}