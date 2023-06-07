using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Queries.GetList;

public sealed class GetLessonTemplateListQueryHandler
    : IRequestHandler<GetLessonTemplateListQuery, PagedList<LessonTemplateViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetLessonTemplateListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<LessonTemplateViewModel>> Handle(GetLessonTemplateListQuery request,
        CancellationToken cancellationToken)
    {
        var templates = await _context.Set<LessonTemplate>()
            .Include(e => e.Discipline)
            .Include(e => e.Time)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var totalCount = await _context.Set<LessonTemplate>().CountAsync(cancellationToken);
        var viewModels = _mapper.Map<List<LessonTemplateViewModel>>(templates);

        return new PagedList<LessonTemplateViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}