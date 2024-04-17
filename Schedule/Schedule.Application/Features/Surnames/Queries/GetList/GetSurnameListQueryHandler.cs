using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Surnames.Queries.GetList;

public sealed class GetSurnameListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetSurnameListQuery, PagedList<SurnameViewModel>>
{
    public async Task<PagedList<SurnameViewModel>> Handle(GetSurnameListQuery request, CancellationToken cancellationToken)
    {
        var query = context.Surnames
            .OrderBy(e => e.Value)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking();

        if (request.Search is not null)
        {
            query = query.Where(e => e.Value.StartsWith(request.Search));
        }

        var surnames = await query
            .ProjectTo<SurnameViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<SurnameViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = surnames
        };
    }
}