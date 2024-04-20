using MediatR;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineNameCommand : IRequest<int>
{
    public required string Name { get; set; } 
}