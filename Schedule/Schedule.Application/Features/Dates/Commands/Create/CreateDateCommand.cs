using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Commands.Create;

public sealed class CreateDateCommand : IRequest<int>, IMapWith<Date>
{
    public DateTime Value { get; set; }

    public int Term { get; set; }

    public int DayId { get; set; }

    public int WeekTypeId { get; set; }

    public bool IsStudy { get; set; }
}