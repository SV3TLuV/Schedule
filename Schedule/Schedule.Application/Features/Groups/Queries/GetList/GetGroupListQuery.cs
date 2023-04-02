using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed record GetGroupListQuery : IRequest<GroupViewModel[]>;