using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommand : IRequest, IMapWith<Group>
{
    public required int Id { get; set; }
    public required string Number { get; set; }
    public required int CourseId { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityCodeId { get; set; }
    public required ICollection<int> MergedGroupIds { get; set; } 
}