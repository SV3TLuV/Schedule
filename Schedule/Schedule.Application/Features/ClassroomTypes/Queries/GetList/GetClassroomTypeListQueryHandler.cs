using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed class GetClassroomTypeListQueryHandler
    : IRequestHandler<GetClassroomTypeListQuery, PagedList<ClassroomTypeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<ClassroomTypeViewModel>> Handle(GetClassroomTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<ClassroomType>()
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null) query = query.Where(e => e.Name.StartsWith(request.Search));

        var classroomTypes = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<ClassroomTypeViewModel[]>(classroomTypes);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<ClassroomTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}