using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateDisciplineCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = _mapper.Map<Discipline>(request);
        await _context.Set<Discipline>().AddAsync(discipline, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return discipline.DisciplineId;
    }
}