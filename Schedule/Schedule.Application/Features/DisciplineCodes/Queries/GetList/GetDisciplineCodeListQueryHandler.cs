using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Queries.GetList;

public sealed class GetDisciplineCodeListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineCodeListQuery, PagedList<DisciplineCodeViewModel>>
{
    public async Task<PagedList<DisciplineCodeViewModel>> Handle(GetDisciplineCodeListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.DisciplineCodes
            .OrderBy(e => e.Code)
            .AsNoTracking();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e => e.Code.StartsWith(request.Search));

        var disciplineCodes = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<DisciplineCodeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DisciplineCodeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = disciplineCodes
        };
    }
}