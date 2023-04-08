using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Dates.Commands.Create;

public sealed class CreateDateCommand : IRequest<int>, IMapWith<Date>
{
    public required DateTime Value { get; set; }
    public required int Term { get; set; }
    public required int WeekTypeId { get; set; }
    public required int DayId { get; set; }
    public required int TimeTypeId { get; set; }
    public required bool IsStudy { get; set; }
}