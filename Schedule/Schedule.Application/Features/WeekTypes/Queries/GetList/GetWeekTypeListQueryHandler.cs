using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.GetList;

public sealed class GetWeekTypeListQueryHandler
    : IRequestHandler<GetWeekTypeListQuery, PagedList<WeekTypeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetWeekTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<WeekTypeViewModel>> Handle(GetWeekTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var weekTypes = await _context.Set<WeekType>()
            .OrderBy(e => e.WeekTypeId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<WeekTypeViewModel[]>(weekTypes);
        var totalCount = await _context.Set<WeekType>().CountAsync(cancellationToken);

        return new PagedList<WeekTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}