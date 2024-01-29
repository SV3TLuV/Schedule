using MediatR;

namespace Schedule.Application.Features.TimeTypes.Commands.Delete;

public sealed record DeleteTimeTypeCommand(int Id) : IRequest<Unit>;