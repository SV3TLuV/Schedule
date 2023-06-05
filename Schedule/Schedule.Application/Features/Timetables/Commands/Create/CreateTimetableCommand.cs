using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Commands.Create;

[SignalRNotification(typeof(Timetable), CommandTypes.Create)]
public sealed class CreateTimetableCommand : IRequest<int>, IMapWith<Timetable>
{
    public int GroupId { get; set; }
    public int DateId { get; set; }
}