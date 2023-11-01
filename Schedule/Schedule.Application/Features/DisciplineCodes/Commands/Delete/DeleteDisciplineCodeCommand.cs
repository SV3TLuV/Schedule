using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

[SignalRNotification(typeof(DisciplineCode), CommandTypes.Delete)]
public sealed record DeleteDisciplineCodeCommand(int Id) : IRequest<Unit>;