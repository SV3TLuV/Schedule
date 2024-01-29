using MediatR;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

public sealed record DeleteSpecialityCommand(int Id) : IRequest<Unit>;