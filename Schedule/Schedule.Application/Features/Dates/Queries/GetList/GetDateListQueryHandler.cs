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
        var dates = await _context.Set<Date>()
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .Include(e => e.TimeType)
            .Skip((request.Page - 1) * request.Count)
            .Take(request.Count)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        
        var viewModels = _mapper.Map<List<DateViewModel>>(dates);
        var totalCount = await _context.Set<Date>().CountAsync(cancellationToken);

        return new PagedList<DateViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}