using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommandHandler(IDisciplineCodeRepository disciplineCodeRepository)
    : IRequestHandler<CreateDisciplineCodeCommand, int>
{
    public async Task<int> Handle(CreateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        return await disciplineCodeRepository.AddIfNotExistAsync(request.Code, cancellationToken);
    }
}