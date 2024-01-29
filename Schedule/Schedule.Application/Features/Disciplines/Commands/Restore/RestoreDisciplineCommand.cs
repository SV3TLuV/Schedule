using MediatR;

namespace Schedule.Application.Features.Disciplines.Commands.Restore;

public sealed record RestoreDisciplineCommand(int Id) : IRequest<Unit>;