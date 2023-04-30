using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Queries.GetList;

public sealed class GetLessonListQueryHandler 
    : IRequestHandler<GetLessonListQuery, PagedList<LessonViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetLessonListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<LessonViewModel>> Handle(GetLessonListQuery request,
        CancellationToken cancellationToken)
    {
        var lessons = await _context.Set<Lesson>()
            .Include(e => e.Discipline)
            .Include(e => e.Time)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var totalCount = await _context.Set<Lesson>().CountAsync(cancellationToken);
        var viewModels = _mapper.Map<List<LessonViewModel>>(lessons);

        return new PagedList<LessonViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}