using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
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
        var query = _context.Set<TimeType>()
            .OrderBy(e => e.TimeTypeId)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };
        
        if (request.Search is not null)
        {
            query = query.Where(e => e.Name.StartsWith(request.Search));
        }

        var timeTypes = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);
        var viewModels = _mapper.Map<TimeTypeViewModel[]>(timeTypes);
        var totalCount = await _context.Set<TimeType>().CountAsync(cancellationToken);

        return new PagedList<TimeTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}