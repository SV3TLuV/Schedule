using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed class GetClassroomListQueryHandler
    : IRequestHandler<GetClassroomListQuery, PagedList<ClassroomViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<ClassroomViewModel>> Handle(GetClassroomListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Classroom>()
            .Include(e => e.ClassroomClassroomTypes)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        var classrooms = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<ClassroomViewModel[]>(classrooms);
        var totalCount = await _context.Set<Classroom>().CountAsync(cancellationToken);

        return new PagedList<ClassroomViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}