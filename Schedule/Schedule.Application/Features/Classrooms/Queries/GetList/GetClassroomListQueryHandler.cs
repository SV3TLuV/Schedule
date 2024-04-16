using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed class GetClassroomListQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetClassroomListQuery, PagedList<ClassroomViewModel>>
{
    public async Task<PagedList<ClassroomViewModel>> Handle(GetClassroomListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Classrooms
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null) query = query.Where(e => e.Cabinet.StartsWith(request.Search));

        var classrooms = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ClassroomViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<ClassroomViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = classrooms
        };
    }
}