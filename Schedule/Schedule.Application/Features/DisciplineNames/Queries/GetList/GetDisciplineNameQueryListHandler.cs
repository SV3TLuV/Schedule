using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Queries.GetList;

public sealed class GetDisciplineNameQueryListHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineNameQueryList, PagedList<DisciplineNameViewModel>>
{
    public async Task<PagedList<DisciplineNameViewModel>> Handle(GetDisciplineNameQueryList request,
        CancellationToken cancellationToken)
    {
        var query = context.DisciplineNames
            .OrderBy(e => e.Name)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e => e.Name.StartsWith(request.Search));

        var disciplineNames = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<DisciplineNameViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DisciplineNameViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = disciplineNames
        };
    }
}