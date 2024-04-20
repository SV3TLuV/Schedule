using MediatR;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommand : IRequest<int>
{
    public required string Code { get; set; } 
}