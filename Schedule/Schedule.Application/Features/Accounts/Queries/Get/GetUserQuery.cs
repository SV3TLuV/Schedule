using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Users.Queries.Get;

public sealed record GetUserQuery(int Id) : IRequest<UserViewModel>;