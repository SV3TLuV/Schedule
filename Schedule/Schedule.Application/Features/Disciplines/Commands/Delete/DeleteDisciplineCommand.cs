using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

[SignalRNotification(typeof(Discipline), CommandTypes.Delete)]
public sealed record DeleteDisciplineCommand(int Id) : IRequest<Unit>;