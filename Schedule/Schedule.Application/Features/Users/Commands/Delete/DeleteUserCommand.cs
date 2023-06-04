using MediatR;

namespace Schedule.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(int Id) : IRequest;