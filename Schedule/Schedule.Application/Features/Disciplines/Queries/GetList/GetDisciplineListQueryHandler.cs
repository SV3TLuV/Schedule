using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed class GetDisciplineListQueryHandler
    : IRequestHandler<GetDisciplineListQuery, DisciplineViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DisciplineViewModel[]> Handle(GetDisciplineListQuery request,
        CancellationToken cancellationToken)
    {
        var disciplines = await _context.Set<Discipline>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Code)
            .ToListAsync(cancellationToken);
        return _mapper.Map<DisciplineViewModel[]>(disciplines);
    }
}