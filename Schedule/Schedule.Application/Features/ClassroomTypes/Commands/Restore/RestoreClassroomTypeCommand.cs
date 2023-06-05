using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Restore;

[SignalRNotification(typeof(ClassroomType), CommandTypes.Restore)]
public sealed record RestoreClassroomTypeCommand(int Id) : IRequest<Unit>;