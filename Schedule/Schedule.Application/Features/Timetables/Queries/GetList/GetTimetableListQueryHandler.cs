using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public sealed class GetTimetableListQueryHandler
    : IRequestHandler<GetTimetableListQuery, PagedList<TimetableViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimetableListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TimetableViewModel>> Handle(GetTimetableListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Timetable>()
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
            .Include(e => e.Date)
            .ThenInclude(e => e.Day)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Time)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .OrderBy(e => e.TimetableId)
            .Where(e => !e.Group.IsDeleted)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery();

        if (request.DateId is not null) query = query.Where(e => e.DateId == request.DateId);

        if (request.GroupId is not null) query = query.Where(e => e.GroupId == request.GroupId);

        var timetables = await query
            .OrderBy(e => e.Group.TermId)
            .ThenBy(e => string.Concat(e.Group.Speciality.Name, "-", e.Group.Number))
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TimetableViewModel>>(timetables);
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

        return new PagedList<TimetableViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModelsResult
        };
    }
}