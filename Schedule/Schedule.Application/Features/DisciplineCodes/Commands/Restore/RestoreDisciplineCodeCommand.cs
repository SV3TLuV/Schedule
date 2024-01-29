using MediatR;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

public sealed record RestoreDisciplineCodeCommand(int Id) : IRequest<Unit>;