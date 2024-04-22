using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed class GetGroupListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetGroupListQuery, PagedList<GroupViewModel>>
{
    public async Task<PagedList<GroupViewModel>> Handle(GetGroupListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Groups
            .Include(e => e.Term)
            .Include(e => e.Speciality)
            .ThenInclude(e => e.Disciplines)
            .ThenInclude(e => e.Term)
            .OrderBy(e => e.Term.Course)
            .ThenBy(e => e.Speciality.Code)
            .AsSplitQuery()
            .AsNoTracking();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e =>
                (e.Speciality.Name + '-' + e.Number).StartsWith(request.Search) ||
                e.Speciality.Code.StartsWith(request.Search) ||
                e.Number.StartsWith(request.Search));

        var groups = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<GroupViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<GroupViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = groups
        };
    }
}