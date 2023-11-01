using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

[SignalRNotification(typeof(DisciplineCode), CommandTypes.Restore)]
public sealed record RestoreDisciplineCodeCommand(int Id) : IRequest<Unit>;