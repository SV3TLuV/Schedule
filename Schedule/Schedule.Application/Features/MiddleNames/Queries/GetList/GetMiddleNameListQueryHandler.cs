using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.MiddleNames.Queries.GetList;

public sealed class GetMiddleNameListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetMiddleNameListQuery, PagedList<MiddleNameViewModel>>
{
    public async Task<PagedList<MiddleNameViewModel>> Handle(GetMiddleNameListQuery request, CancellationToken cancellationToken)
    {
        var query = context.MiddleNames
            .OrderBy(e => e.Value)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking();

        if (request.Search is not null)
        {
            query = query.Where(e => e.Value.StartsWith(request.Search));
        }

        var middleNames = await query
            .ProjectTo<MiddleNameViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<MiddleNameViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = middleNames
        };
    }
}