using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.UpdateFilledLessonsTimeCommand;

[SignalRNotification(typeof(Lesson), CommandTypes.Update)]
public sealed class UpdateFilledLessonsTimeCommand : IRequest<Unit>
{
    public required int DateId { get; set; }
    public required int TimeTypeId { get; set; }
    public ICollection<int>? PairNumbers { get; set; }
}