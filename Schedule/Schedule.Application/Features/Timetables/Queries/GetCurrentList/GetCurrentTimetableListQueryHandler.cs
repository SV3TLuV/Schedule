using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.EqualityComparers;
using Schedule.Application.Common.Interfaces;
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
            .ThenInclude(e => e.Day)
            .Include(e => e.Date)
            .ThenInclude(e => e.WeekType)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Name)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Code)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.DisciplineType)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Time)
            .ThenInclude(e => e.Type)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Where(e => dateIds.Contains(e.DateId))
            .Where(e => !e.Group.IsDeleted)
            .AsSplitQuery()
            .AsNoTracking();

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
        var hashSet = new HashSet<ViewModelInfo>();

        foreach (var viewModel in viewModels)
        {
            var infos = viewModel.Groups
                .DistinctBy(g => g.Id)
                .Select(g => new ViewModelInfo
                {
                    GroupId = g.Id, 
                    DateId = viewModel.Date.Id
                })
                .ToList();

            var hasDuplicate = false;

            foreach (var info in infos)
            {
                if (hashSet.Contains(info))
                {
                    hasDuplicate = true;
                    break;
                }

                hashSet.Add(info);
            }

            if (hasDuplicate)
            {
                viewModelIdsForRemove.Add(viewModel.Id);
            }
        }

        var viewModelsResult = viewModels
            .Where(v => !viewModelIdsForRemove.Contains(v.Id))
            .ToArray();

        var groupedViewModels = viewModelsResult
            .GroupBy(viewModel => viewModel.Groups, new GroupViewModelsEqualityComparer())
            .ToArray();

        var currentTimetables = new List<CurrentTimetableViewModel>();

        foreach (var groupedViewModel in groupedViewModels)
        {
            var groupedTimetableViewModels = groupedViewModel
                .GroupBy(timetable => timetable.Date)
                .Select(grouping => new GroupedViewModel<DateViewModel, TimetableViewModel>
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
            .AsNoTracking()
            .Where(e =>
                e.Value.Date >= currentDate.Value.Date &&
                e.Day.IsStudy)
            .OrderBy(e => e.Value)
            .Select(e => e.DateId)
            .Take(request.DateCount)
            .ToListAsync(cancellationToken);
    }

    private sealed record ViewModelInfo
    {
        public int GroupId { get; set; }
        public int DateId { get; set; }
    }
}