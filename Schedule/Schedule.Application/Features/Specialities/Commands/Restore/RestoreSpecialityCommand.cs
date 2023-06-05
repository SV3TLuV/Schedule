using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Restore;

[SignalRNotification(typeof(Speciality), CommandTypes.Restore)]
public sealed record RestoreSpecialityCommand(int Id) : IRequest<Unit>;