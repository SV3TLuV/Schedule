using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.GetList;

public sealed class GetWeekTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetWeekTypeListQuery, PagedList<WeekTypeViewModel>>
{
    public async Task<PagedList<WeekTypeViewModel>> Handle(GetWeekTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var weekTypes = await context.WeekTypes
            .OrderBy(e => e.WeekTypeId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ProjectTo<WeekTypeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.WeekTypes.CountAsync(cancellationToken);

        return new PagedList<WeekTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = weekTypes
        };
    }
}