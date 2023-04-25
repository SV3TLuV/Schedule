using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Commands.Create;

public sealed class CreateTimetableCommand : IRequest<int>, IMapWith<Timetable>
{
    public int GroupId { get; set; }
    public int DateId { get; set; }
}