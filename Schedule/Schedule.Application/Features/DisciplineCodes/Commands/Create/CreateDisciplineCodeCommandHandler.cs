using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommandHandler : IRequestHandler<CreateDisciplineCodeCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateDisciplineCodeCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var searched = await _context.Set<DisciplineCode>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Code == request.Code, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Код дисциплины: {searched.Code}");
        
        var disciplineCode = _mapper.Map<DisciplineCode>(request);
        await _context.Set<DisciplineCode>().AddAsync(disciplineCode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return disciplineCode.DisciplineCodeId;
    }
}