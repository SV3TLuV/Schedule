using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Create;

public sealed class CreateLessonCommand : IRequest<int>, IMapWith<Lesson>
{
    public required int Number { get; set; }
    public required int? Subgroup { get; set; }
    public required int? TimeId { get; set; }
    public required int TimetableId { get; set; }
    public required int? DisciplineId { get; set; }
    public required ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; } 
}