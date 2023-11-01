using MediatR;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineNameCommandHandler : IRequestHandler<CreateDisciplineNameCommand, int>
{
    private readonly IScheduleDbContext _context;

    public CreateDisciplineNameCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Handle(CreateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}