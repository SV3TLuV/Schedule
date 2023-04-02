using MediatR;

namespace Schedule.Application.Features.WeekTypes.Commands.Delete;

public sealed record DeleteWeekTypeCommand(int Id) : IRequest;