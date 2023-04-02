using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed class GetDayListQueryHandler 
    : IRequestHandler<GetDayListQuery, DayViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDayListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DayViewModel[]> Handle(GetDayListQuery request,
        CancellationToken cancellationToken)
    {
        var days = await _context.Days
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DayId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<DayViewModel[]>(days);
    }
}