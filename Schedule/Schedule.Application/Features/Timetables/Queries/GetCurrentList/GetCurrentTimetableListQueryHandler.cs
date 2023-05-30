using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentList;

public sealed class GetCurrentTimetableListQueryHandler
    : IRequestHandler<GetCurrentTimetableListQuery, PagedList<CurrentTimetableViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;

    public GetCurrentTimetableListQueryHandler(IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMapper mapper)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mapper = mapper;
    }

    public async Task<PagedList<CurrentTimetableViewModel>> Handle(GetCurrentTimetableListQuery request,
        CancellationToken cancellationToken)
    {
        var dateIds = await GetDateIdsAsync(request, cancellationToken);

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
            .Where(e => dateIds.Contains(e.DateId))
            .Where(e => !e.Group.IsDeleted)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery();

        if (request.GroupId is not null)
        {
            query = query.Where(e => e.GroupId == request.GroupId);
        }

        var timetables = await query
            .OrderBy(e => e.Group.TermId)
            .ThenBy(e => string.Concat(e.Group.Speciality.Name, "-", e.Group.Number))
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TimetableViewModel>>(timetables);

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
        
        var groupedViewModels = viewModelsResult
            .GroupBy(viewModel => viewModel.Groups)
            .ToArray();

        var currentTimetables = new List<CurrentTimetableViewModel>();

        foreach (var groupedViewModel in groupedViewModels)
        {
            var groupedTimetableViewModels = groupedViewModel
                .GroupBy(timetable => timetable.Date)
                .Select(grouping => new GroupedViewModel<DateViewModel, TimetableViewModel>()
                {
                    Key = grouping.Key,
                    Items = grouping.ToArray()
                })
                .ToArray();

            currentTimetables.Add(new CurrentTimetableViewModel
            {
                Groups = groupedViewModel.Key,
                Dates = groupedTimetableViewModels
            });
        }

        var currentViewModels = currentTimetables
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArray(); 
        var totalCount = viewModels.Count;
        
        return new PagedList<CurrentTimetableViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = currentViewModels
        };
    }

    private async Task<ICollection<int>> GetDateIdsAsync(GetCurrentTimetableListQuery request,
        CancellationToken cancellationToken = default)
    {
        var currentDate = _dateInfoService.CurrentDate;
        return await _context.Set<Date>()
            .Include(e => e.Day)
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.Value.Date >= currentDate.Value.Date &&
                e.Day.IsStudy)
            .OrderBy(e => e.Value)
            .Select(e => e.DateId)
            .Take(request.DateCount)
            .ToListAsync(cancellationToken);
    }
}