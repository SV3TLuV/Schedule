using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

[SignalRNotification(typeof(Classroom), CommandTypes.Restore)]
public sealed record RestoreClassroomCommand(int Id) : IRequest<Unit>;