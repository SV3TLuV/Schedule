using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Queries.GetList;

public sealed class GetDateListQueryHandler
    : IRequestHandler<GetDateListQuery, PagedList<DateViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDateListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<DateViewModel>> Handle(GetDateListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Date>()
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .OrderByDescending(e => e.Value.Date)
            .AsNoTrackingWithIdentityResolution();

        if (request.Search is not null)
        {
            query = query.Where(e => e.Value.ToString().Contains(request.Search));
        }

        if (request.EducationalOnly)
        {
            query = query.Where(e => e.Day.IsStudy);
        }
        
        var dates = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<DateViewModel>>(dates);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DateViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}