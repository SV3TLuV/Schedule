using MediatR;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

public sealed record DeleteDisciplineNameCommand(int Id) : IRequest<Unit>;