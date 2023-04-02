using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommand : IRequest, IMapWith<Group>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Course { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityCodeId { get; set; }
}