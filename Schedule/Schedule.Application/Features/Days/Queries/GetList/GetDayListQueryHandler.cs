using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed class GetDayListQueryHandler
    : IRequestHandler<GetDayListQuery, DayViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDayListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DayViewModel[]> Handle(GetDayListQuery request,
        CancellationToken cancellationToken)
    {
        var days = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DayId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<DayViewModel[]>(days);
    }
}