using MediatR;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

public sealed record DeleteClassroomCommand(int Id) : IRequest;