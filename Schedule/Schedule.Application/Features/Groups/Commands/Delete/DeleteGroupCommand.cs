using MediatR;

namespace Schedule.Application.Features.Groups.Commands.Delete;

public sealed record DeleteGroupCommand(int Id) : IRequest;