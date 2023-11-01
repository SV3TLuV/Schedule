using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineNameCommandHandler : IRequestHandler<CreateDisciplineNameCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateDisciplineNameCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        var searched = await _context.Set<DisciplineName>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Name == request.Name);

        if (searched is not null)
            throw new AlreadyExistsException($"Название дисциплины: {searched.Name}");

        var disciplineName = _mapper.Map<DisciplineName>(request);
        await _context.Set<DisciplineName>().AddAsync(disciplineName, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return disciplineName.DisciplineNameId;
    }
}