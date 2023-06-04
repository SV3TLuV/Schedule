using MediatR;

namespace Schedule.Application.Features.Times.Commands.Restore;

public sealed record RestoreTimeCommand(int Id) : IRequest;