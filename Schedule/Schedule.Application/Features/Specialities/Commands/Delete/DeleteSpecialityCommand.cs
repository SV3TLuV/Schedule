using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

[SignalRNotification(typeof(Speciality), CommandTypes.Delete)]
public sealed record DeleteSpecialityCommand(int Id) : IRequest<Unit>;