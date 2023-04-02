using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.Times.Queries.GetList;

public sealed class GetTimeListQueryHandler : IRequestHandler<GetTimeListQuery, TimeViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TimeViewModel[]> Handle(GetTimeListQuery request,
        CancellationToken cancellationToken)
    {
        var times = await _context.Times
            .Include(e => e.Type)
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.TypeId)
            .ThenBy(e => e.LessonNumber)
            .ToListAsync(cancellationToken);
        return _mapper.Map<TimeViewModel[]>(times);
    }
}