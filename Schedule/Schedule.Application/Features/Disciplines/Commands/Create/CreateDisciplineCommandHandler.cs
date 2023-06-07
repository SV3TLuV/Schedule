using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
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
        var searched = await _context.Set<Discipline>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.TermId == request.TermId &&
                e.Name == request.Name &&
                e.Code == request.Code &&
                e.SpecialityId == request.SpecialityId, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Дисциплина: {searched.Name}");
        
        var discipline = _mapper.Map<Discipline>(request);
        await _context.Set<Discipline>().AddAsync(discipline, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return discipline.DisciplineId;
    }
}