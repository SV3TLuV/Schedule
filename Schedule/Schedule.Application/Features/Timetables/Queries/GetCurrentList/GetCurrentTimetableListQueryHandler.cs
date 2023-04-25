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
        var currentDate = _dateInfoService.CurrentDate;
        var dateIds = await _context.Set<Date>()
            .Include(e => e.Day)
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.Value.Date >= currentDate.Value.Date &&
                e.Day.IsStudy)
            .OrderBy(e => e.Value)
            .Select(e => e.DateId)
            .Take(request.DateCount)
            .ToListAsync(cancellationToken);

        var query = _context.Set<Timetable>()
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
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
            .AsNoTrackingWithIdentityResolution()
            .Where(e => dateIds.Contains(e.DateId));

        if (request.GroupId is not null) query = query.Where(e => e.GroupId == request.GroupId);

        var timetables = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TimetableViewModel>>(timetables);
        var grouped = viewModels.GroupBy(t => t.Group);

        var currentTimetables = new List<CurrentTimetableViewModel>();

        foreach (var group in grouped)
        {
            var groupedViewModels = group
                .GroupBy(t => t.Date)
                .Select(g =>
                    new GroupedViewModel<DateViewModel, TimetableViewModel>
                    {
                        Key = g.Key,
                        Items = g.ToArray()
                    })
                .ToArray();

            currentTimetables.Add(new CurrentTimetableViewModel
            {
                Group = group.Key,
                Dates = groupedViewModels
            });
        }

        var totalCount = await _context.Set<Group>().CountAsync(cancellationToken);

        return new PagedList<CurrentTimetableViewModel>
        {
            PageSize = request.DateCount,
            PageNumber = 1,
            TotalCount = totalCount,
            Items = currentTimetables
        };
    }
}