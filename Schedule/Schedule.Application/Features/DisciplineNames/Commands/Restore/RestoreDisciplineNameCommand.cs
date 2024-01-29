using MediatR;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

public sealed record RestoreDisciplineNameCommand(int Id) : IRequest<Unit>;