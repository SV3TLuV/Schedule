using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Restore;

[SignalRNotification(typeof(Discipline), CommandTypes.Restore)]
public sealed record RestoreDisciplineCommand(int Id) : IRequest<Unit>;