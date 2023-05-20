using MediatR;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

public sealed record RestoreClassroomCommand(int Id) : IRequest;