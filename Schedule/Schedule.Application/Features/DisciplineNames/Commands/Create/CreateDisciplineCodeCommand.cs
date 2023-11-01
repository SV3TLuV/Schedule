using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineCodeCommand : IRequest<int>, IMapWith<DisciplineCode>
{
    
}

public sealed class CreateDisciplineCodeCommandHandler : IRequestHandler<CreateDisciplineCodeCommand, int>
{
    public Task<int> Handle(CreateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}