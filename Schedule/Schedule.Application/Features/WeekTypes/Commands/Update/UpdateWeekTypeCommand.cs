using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.WeekTypes.Commands.Update;

public sealed class UpdateWeekTypeCommand : IRequest, IMapWith<WeekType>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}