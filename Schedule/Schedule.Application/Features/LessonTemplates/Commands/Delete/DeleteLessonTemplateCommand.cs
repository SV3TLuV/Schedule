using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Delete;

[SignalRNotification(typeof(LessonTemplate), CommandTypes.Delete)]
public sealed record DeleteLessonTemplateCommand(int Id) : IRequest<Unit>;