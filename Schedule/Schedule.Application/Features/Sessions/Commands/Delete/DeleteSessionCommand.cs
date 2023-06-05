using MediatR;

namespace Schedule.Application.Features.Sessions.Commands.Delete;

public sealed record DeleteSessionCommand(Guid Id) : IRequest<Unit>;