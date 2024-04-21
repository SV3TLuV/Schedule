using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.EqualityComparers;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentTimetableList;

public sealed class GetCurrentTimetableListQueryHandler(
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    IMapper mapper)
    : IRequestHandler<GetCurrentTimetableListQuery, PagedList<CurrentTimetableViewModel>>
{
    public async Task<PagedList<CurrentTimetableViewModel>> Handle(GetCurrentTimetableListQuery request,
        CancellationToken cancellationToken)
    {
        var tomorrow = dateInfoService.CurrentDate.AddDays(-1);
        var dayIds = GetDayIds(request.DayCount);

        var query = context.Timetables
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .Include(e => e.Group)
                .ThenInclude(e => e.Speciality)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e!.Name)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e!.Code)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e!.Type)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonTeacherClassrooms)
                .ThenInclude(e => e.Teacher)
                .ThenInclude(e => e.Account)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonTeacherClassrooms)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date >= tomorrow))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Name)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date >= tomorrow))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Code)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date >= tomorrow))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e!.Type)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date >= tomorrow))
                .ThenInclude(e => e.LessonChangeTeacherClassrooms)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date >= tomorrow))
                .ThenInclude(e => e.LessonChangeTeacherClassrooms)
                .ThenInclude(e => e.Teacher)
                .ThenInclude(e => e.Account)
            .Where(e => dayIds.Contains(e.DayId))
            .Where(e => e.Ended == null)
            .Where(e => !e.Group.IsDeleted)
            .AsSplitQuery()
            .AsNoTracking();

        if (request.GroupId is not null)
        {
            query = query.Where(e => e.GroupId == request.GroupId);
        }

        var timetables = await query
            .OrderBy(e => e.Group.TermId)
            .ThenBy(e => e.Group.Name)
            .ProjectTo<TimetableViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var groupedViewModels = timetables
            .GroupBy(vm => vm.Group, new GroupViewModelByIdEqualityComparer())
            .ToArray();

        var currentTimetables = new List<CurrentTimetableViewModel>();

        foreach (var groupedViewModel in groupedViewModels)
        {
            var groupedTimetableViewModels = groupedViewModel
                .GroupBy(timetable => timetable.Day)
                .Select(grouping => new GroupedViewModel<DayViewModel, TimetableViewModel>
                {
                    Key = grouping.Key,
                    Items = grouping.ToArray()
                })
                .OrderBy(e => Array.IndexOf(dayIds, e.Key.Id))
                .ToArray();

            currentTimetables.Add(new CurrentTimetableViewModel
            {
                Group = groupedViewModel.Key,
                Days = groupedTimetableViewModels
            });
        }

        var currentViewModels = currentTimetables
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArray();
        var totalCount = timetables.Count;

        return new PagedList<CurrentTimetableViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = currentViewModels
        };
    }

    private int[] GetDayIds(int count)
    {
        var ids = new int[count];

        var id = dateInfoService.CurrentDayId;

        for (var i = 0; i < count; i++)
        {
            ids[i] = (id + i - 1) % 7 + 1;
        }

        return ids;
    }
}