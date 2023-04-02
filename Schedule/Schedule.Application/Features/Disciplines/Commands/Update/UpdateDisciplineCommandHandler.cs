using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommandHandler : IRequestHandler<UpdateDisciplineCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDisciplineCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var disciplineDbo = await _context.Disciplines
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);
        
        if (disciplineDbo is null)
            throw new NotFoundException(nameof(Discipline), request.Id);
        
        var discipline = _mapper.Map<Discipline>(request);
        _context.Disciplines.Update(discipline);
        await _context.SaveChangesAsync(cancellationToken);
    }
}