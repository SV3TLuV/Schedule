using MediatR;

namespace Schedule.Application.Features.Lessons.Commands.UpdateFilledLessonsTimeCommand;

public sealed class UpdateFilledLessonsTimeCommand : IRequest<Unit>
{
    public required int DateId { get; set; }
    public required int TimeTypeId { get; set; }
    public ICollection<int>? PairNumbers { get; set; }
}