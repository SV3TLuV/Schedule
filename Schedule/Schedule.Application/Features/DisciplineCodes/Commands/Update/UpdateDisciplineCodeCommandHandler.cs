using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

public sealed class UpdateDisciplineCodeCommandHandler : IRequestHandler<UpdateDisciplineCodeCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDisciplineCodeCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCodeDbo = await _context.Set<DisciplineCode>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCodeDbo is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        var disciplineCode = _mapper.Map<DisciplineCode>(request);
        _context.Set<DisciplineCode>().Update(disciplineCode);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}