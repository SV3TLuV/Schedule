using MediatR;

namespace Schedule.Application.Features.TimeTypes.Commands.Restore;

public sealed record RestoreTimeTypeCommand(int Id) : IRequest;