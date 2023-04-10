using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed class GetTimeTypeListQueryHandler 
    : IRequestHandler<GetTimeTypeListQuery, PagedList<TimeTypeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TimeTypeViewModel>> Handle(GetTimeTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var timeTypes = await _context.Set<TimeType>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.TimeTypeId)
            .ToListAsync(cancellationToken);
        
        var viewModels = _mapper.Map<TimeTypeViewModel[]>(timeTypes);
        var totalCount = await _context.Set<TimeType>().CountAsync(cancellationToken);

        return new PagedList<TimeTypeViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}