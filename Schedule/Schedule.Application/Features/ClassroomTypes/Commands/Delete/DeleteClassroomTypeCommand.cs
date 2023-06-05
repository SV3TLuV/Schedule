using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Delete;

[SignalRNotification(typeof(ClassroomType), CommandTypes.Delete)]
public sealed record DeleteClassroomTypeCommand(int Id) : IRequest<Unit>;