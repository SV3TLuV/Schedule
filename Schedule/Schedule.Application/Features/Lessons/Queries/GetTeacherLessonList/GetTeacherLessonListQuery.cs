using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Lessons.Queries.GetTeacherLessonList;

public sealed record GetTeacherLessonListQuery : IRequest<LessonViewModel[]>
{
    public int TeacherId { get; set; }
    public DateOnly Date { get; set; }
}