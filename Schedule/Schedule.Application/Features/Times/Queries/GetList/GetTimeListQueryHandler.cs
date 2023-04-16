using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Queries.GetList;

public sealed class GetTimeListQueryHandler : IRequestHandler<GetTimeListQuery, PagedList<TimeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TimeViewModel>> Handle(GetTimeListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Time>()
            .Include(e => e.Type)
            .OrderBy(e => e.TypeId)
            .ThenBy(e => e.LessonNumber)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        var times = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<TimeViewModel[]>(times);
        var totalCount = await _context.Set<Time>().CountAsync(cancellationToken);

        return new PagedList<TimeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}