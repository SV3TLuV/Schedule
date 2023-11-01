using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public sealed class UpdateDisciplineNameCommandHandler : IRequestHandler<UpdateDisciplineNameCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDisciplineNameCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        var disciplineNameDbo = await _context.Set<DisciplineName>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineNameDbo is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        var disciplineName = _mapper.Map<DisciplineName>(request);
        _context.Set<DisciplineName>().Update(disciplineName);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}