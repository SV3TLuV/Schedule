using MediatR;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed record DeleteDisciplineCommand(int Id) : IRequest<Unit>;