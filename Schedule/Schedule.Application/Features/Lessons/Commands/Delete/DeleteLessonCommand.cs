using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Delete;

[SignalRNotification(typeof(Lesson), CommandTypes.Delete)]
public sealed record DeleteLessonCommand(int Id) : IRequest<Unit>;