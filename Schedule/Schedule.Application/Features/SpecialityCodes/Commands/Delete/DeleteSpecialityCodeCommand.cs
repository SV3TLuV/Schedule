using MediatR;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Delete;

public sealed record DeleteSpecialityCodeCommand(int Id) : IRequest;