using MediatR;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommandHandler : IRequestHandler<CreateDisciplineCodeCommand, int>
{
    public Task<int> Handle(CreateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}