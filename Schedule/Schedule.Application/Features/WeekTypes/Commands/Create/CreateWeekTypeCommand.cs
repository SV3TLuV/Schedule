using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.WeekTypes.Commands.Create;

public sealed class CreateWeekTypeCommand : IRequest<int>, IMapWith<WeekType>
{
    public required string Name { get; set; }
}