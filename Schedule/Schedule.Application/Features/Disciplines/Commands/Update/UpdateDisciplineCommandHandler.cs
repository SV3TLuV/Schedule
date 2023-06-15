using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommandHandler : IRequestHandler<UpdateDisciplineCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDisciplineCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var disciplineDbo = await _context.Set<Discipline>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (disciplineDbo is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        var discipline = _mapper.Map<Discipline>(request);
        _context.Set<Discipline>().Update(discipline);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}