using MediatR;

namespace Schedule.Application.Features.Specialities.Commands.Restore;

public sealed record RestoreSpecialityCommand(int Id) : IRequest<Unit>;