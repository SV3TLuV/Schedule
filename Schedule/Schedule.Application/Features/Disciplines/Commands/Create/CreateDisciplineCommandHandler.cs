using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateDisciplineCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = _mapper.Map<Discipline>(request);
        await _context.Disciplines.AddAsync(discipline, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return discipline.DisciplineId;
    }
}