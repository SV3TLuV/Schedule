using MediatR;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Restore;

public sealed record RestoreClassroomTypeCommand(int Id) : IRequest;