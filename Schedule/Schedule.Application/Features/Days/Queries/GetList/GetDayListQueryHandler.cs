using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed class GetDayListQueryHandler : IRequestHandler<GetDayListQuery, PagedList<DayViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDayListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<DayViewModel>> Handle(GetDayListQuery request,
        CancellationToken cancellationToken)
    {
        var days = await _context.Set<Day>()
            .OrderBy(e => e.DayId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<DayViewModel>>(days);
        var totalCount = await _context.Set<Day>().CountAsync(cancellationToken);

        return new PagedList<DayViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}