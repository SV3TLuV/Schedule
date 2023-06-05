using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Delete;

[SignalRNotification(typeof(Teacher), CommandTypes.Delete)]
public sealed record DeleteTeacherCommand(int Id) : IRequest<Unit>;