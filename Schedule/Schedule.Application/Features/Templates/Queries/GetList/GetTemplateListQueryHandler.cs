using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Queries.GetList;

public sealed class GetTemplateListQueryHandler : IRequestHandler<GetTemplateListQuery, PagedList<TemplateViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTemplateListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TemplateViewModel>> Handle(GetTemplateListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Template>()
            .Include(e => e.Day)
            .Include(e => e.Term)
            .Include(e => e.WeekType)
            .Include(e => e.Group)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Time)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution();

        if (request.WeekTypeId is not null) query = query.Where(e => e.WeekTypeId == request.WeekTypeId);

        if (request.TermId is not null) query = query.Where(e => e.TermId == request.TermId);

        if (request.DayId is not null) query = query.Where(e => e.DayId == request.DayId);

        if (request.GroupId is not null) query = query.Where(e => e.GroupId == request.GroupId);

        var templates = await query
            .OrderBy(e => e.Group.TermId)
            .ThenBy(e => string.Concat(e.Group.Speciality.Name, "-", e.Group.Number))
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TemplateViewModel>>(templates);
        var totalCount = await query.CountAsync(cancellationToken);

        var viewModelIdsForRemove = new List<int>();
        var groupIds = new List<int>();

        foreach (var viewModel in viewModels)
        {
            var viewModelGroupIds = viewModel.Groups
                .Select(g => g.Id)
                .ToArray();

            if (viewModelGroupIds.Any(id => groupIds.Contains(id)))
                viewModelIdsForRemove.Add(viewModel.Id);
            else
                groupIds.AddRange(viewModelGroupIds);
        }

        var viewModelsResult = viewModels
            .Where(v => !viewModelIdsForRemove.Contains(v.Id))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArray();

        return new PagedList<TemplateViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModelsResult
        };
    }
}