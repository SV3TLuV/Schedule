using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Groups.Queries.GetAvailableForJoin;

public sealed record GetAvailableForJoinGroupQuery : IRequest<GroupViewModel[]>
{
    public int? GroupId { get; set; }
    public int EnrollmentYear { get; set; }
    public int SpecialityId { get; set; }
    public bool IsAfterEleven { get; set; }
}