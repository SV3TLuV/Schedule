using MediatR;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public sealed record RestoreGroupCommand(int Id) : IRequest<Unit>;