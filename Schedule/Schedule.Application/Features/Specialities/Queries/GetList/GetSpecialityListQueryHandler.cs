using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Queries.GetList;

public sealed class GetSpecialityListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetSpecialityListQuery, PagedList<SpecialityViewModel>>
{
    public async Task<PagedList<SpecialityViewModel>> Handle(GetSpecialityListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Specialities
            .Include(e => e.Disciplines)
            .Include(e => e.Disciplines)
            .OrderBy(e => e.SpecialityId)
            .AsNoTracking();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e =>
                e.Name.StartsWith(request.Search) ||
                e.Code.StartsWith(request.Search));

        var specialities = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<SpecialityViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<SpecialityViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = specialities
        };
    }
}