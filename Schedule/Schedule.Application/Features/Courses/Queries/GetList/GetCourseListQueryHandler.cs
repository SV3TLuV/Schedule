using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Courses.Queries.GetList;

public sealed class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, PagedList<CourseViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<CourseViewModel>> Handle(GetCourseListQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await _context.Set<Course>()
            .AsNoTrackingWithIdentityResolution()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<CourseViewModel>>(courses);
        var totalCount = await _context.Set<CourseViewModel>().CountAsync(cancellationToken);

        return new PagedList<CourseViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}