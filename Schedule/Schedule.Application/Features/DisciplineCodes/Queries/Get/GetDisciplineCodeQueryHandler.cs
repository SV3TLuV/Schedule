using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Queries.Get;

public sealed class GetDisciplineCodeQueryHandler : IRequestHandler<GetDisciplineCodeQuery, DisciplineCodeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineCodeQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DisciplineCodeViewModel> Handle(GetDisciplineCodeQuery request,
        CancellationToken cancellationToken)
    {
        var disciplineCode = await _context.Set<DisciplineCode>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        return _mapper.Map<DisciplineCodeViewModel>(disciplineCode);
    }
}