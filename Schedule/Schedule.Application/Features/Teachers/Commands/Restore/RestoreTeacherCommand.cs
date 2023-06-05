using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Restore;

[SignalRNotification(typeof(Teacher), CommandTypes.Restore)]
public sealed record RestoreTeacherCommand(int Id) : IRequest<Unit>;