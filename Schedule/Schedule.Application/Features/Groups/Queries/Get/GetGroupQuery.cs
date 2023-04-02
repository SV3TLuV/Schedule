using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Groups.Queries.Get;

public sealed record GetGroupQuery(int Id) : IRequest<GroupViewModel>;