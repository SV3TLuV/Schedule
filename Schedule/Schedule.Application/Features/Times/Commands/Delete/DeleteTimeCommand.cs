using MediatR;

namespace Schedule.Application.Features.Times.Commands.Delete;

public sealed record DeleteTimeCommand(int Id) : IRequest<Unit>;