using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Roles.Queries.GetList;

public sealed record GetRoleListQuery : IRequest<ICollection<RoleViewModel>>;