using MediatR;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

public sealed record DeleteDisciplineCodeCommand(int Id) : IRequest<Unit>;