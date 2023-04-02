using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommand : IRequest<int>, IMapWith<Group>
{
    public required string Name { get; set; }
    public required int Course { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityCodeId { get; set; }
}