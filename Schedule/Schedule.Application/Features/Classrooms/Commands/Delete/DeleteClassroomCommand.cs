using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

[SignalRNotification(typeof(Classroom), CommandTypes.Delete)]
public sealed record DeleteClassroomCommand(int Id) : IRequest<Unit>;