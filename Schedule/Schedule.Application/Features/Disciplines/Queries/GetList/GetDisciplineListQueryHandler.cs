using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed class GetDisciplineListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetDisciplineListQuery, PagedList<DisciplineViewModel>>
{
    public async Task<PagedList<DisciplineViewModel>> Handle(GetDisciplineListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Disciplines
            .Include(e => e.Name)
            .Include(e => e.Code)
            .Include(e => e.Type)
            .Include(e => e.Speciality)
            .Include(e => e.Term)
            .ThenInclude(e => e.Course)
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Code)
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e =>
                e.Name.Name.StartsWith(request.Search) ||
                e.Code.Code.StartsWith(request.Search) ||
                e.Speciality.Name.StartsWith(request.Search));

        var disciplines = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = mapper.Map<DisciplineViewModel[]>(disciplines);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DisciplineViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}