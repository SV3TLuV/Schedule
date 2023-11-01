using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

[SignalRNotification(typeof(DisciplineName), CommandTypes.Delete)]
public sealed record DeleteDisciplineNameCommand(int Id) : IRequest<Unit>;