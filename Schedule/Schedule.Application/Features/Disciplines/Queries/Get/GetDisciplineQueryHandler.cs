using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Disciplines.Queries.Get;

public sealed class GetDisciplineQueryHandler : IRequestHandler<GetDisciplineQuery, DisciplineViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DisciplineViewModel> Handle(GetDisciplineQuery request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Disciplines
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        return _mapper.Map<DisciplineViewModel>(discipline);
    }
}