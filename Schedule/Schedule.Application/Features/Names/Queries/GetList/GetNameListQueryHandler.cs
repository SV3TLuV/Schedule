using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Names.Queries.GetList;

public sealed class GetNameListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetNameListQuery, PagedList<NameViewModel>>
{
    public async Task<PagedList<NameViewModel>> Handle(GetNameListQuery request, CancellationToken cancellationToken)
    {
        var query = context.Names
            .OrderBy(e => e.Value)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking();

        if (request.Search is not null)
        {
            query = query.Where(e => e.Value.StartsWith(request.Search));
        }

        var names = await query
            .ProjectTo<NameViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<NameViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = names
        };
    }
}