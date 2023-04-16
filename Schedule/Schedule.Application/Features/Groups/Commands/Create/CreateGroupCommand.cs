using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommand : IRequest<int>, IMapWith<Group>
{
    public required string Number { get; set; }
    public required int CourseId { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityCodeId { get; set; }
}