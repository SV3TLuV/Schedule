using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed class GetDayListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetDayListQuery, PagedList<DayViewModel>>
{
    public async Task<PagedList<DayViewModel>> Handle(GetDayListQuery request,
        CancellationToken cancellationToken)
    {
        var days = await context.Days
            .OrderBy(e => e.DayId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ProjectTo<DayViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.Days.CountAsync(cancellationToken);

        return new PagedList<DayViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = days
        };
    }
}