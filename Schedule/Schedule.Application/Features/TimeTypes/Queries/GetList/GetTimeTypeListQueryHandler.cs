using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed class GetTimeTypeListQueryHandler : IRequestHandler<GetTimeTypeListQuery, TimeTypeViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TimeTypeViewModel[]> Handle(GetTimeTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var timeTypes = await _context.Set<TimeType>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.TimeTypeId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<TimeTypeViewModel[]>(timeTypes);
    }
}