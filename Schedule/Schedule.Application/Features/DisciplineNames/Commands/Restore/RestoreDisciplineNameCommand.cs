using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

[SignalRNotification(typeof(DisciplineName), CommandTypes.Restore)]
public sealed record RestoreDisciplineNameCommand(int Id) : IRequest<Unit>;