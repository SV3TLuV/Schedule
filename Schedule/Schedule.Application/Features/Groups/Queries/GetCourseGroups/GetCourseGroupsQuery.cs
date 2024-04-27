using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Groups.Queries.GetCourseGroups;

public sealed record GetCourseGroupsQuery(int course) : IRequest<ICollection<GroupedViewModel<SpecialityViewModel, GroupViewModel>>>;