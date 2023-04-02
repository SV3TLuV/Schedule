using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed class GetTimeTypeListQueryHandler : IRequestHandler<GetTimeTypeListQuery, TimeTypeViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeTypeListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TimeTypeViewModel[]> Handle(GetTimeTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var timeTypes = await _context.TimeTypes
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.TimeTypeId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<TimeTypeViewModel[]>(timeTypes);
    }
}