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

        var query = context.Timetables
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .Include(e => e.Group)
                .ThenInclude(e => e.Speciality)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Name)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Code)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Type)
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
                .ThenInclude(e => e.Type)
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
            .Where(e =>
                e.Ended == null &&
                !e.Group.IsDeleted)
            .AsSplitQuery()
            .AsNoTracking();

        var dateInfos = GetDateInfos(request.DayCount);

        if (dateInfos.Length == 2)
        {
            query = query.Where(e =>
                dateInfos[0].WeekTypeId == e.WeekTypeId &&
                dateInfos[0].DayIds.Contains(e.DayId) ||
                dateInfos[1].WeekTypeId == e.WeekTypeId &&
                dateInfos[1].DayIds.Contains(e.DayId));
        }
        else
        {
            query = query.Where(e =>
                dateInfos[0].WeekTypeId == e.WeekTypeId &&
                dateInfos[0].DayIds.Contains(e.DayId));
        }

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

        var dayIds = dateInfos.SelectMany(e => e.DayIds).ToArray();

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

    private (int WeekTypeId, int[] DayIds)[] GetDateInfos(int count)
    {
        if (count > 7)
        {
            throw new NotSupportedException($"Count more than 7 not supported.");
        }

        var dayId = dateInfoService.CurrentDayId;
        var weekType = dateInfoService.CurrentWeekType;

        var result = new List<(int, int[])>();
        var dayIds = new int[count];

        for (var i = 0; i < count; i++)
        {
            dayIds[i] = (dayId + i - 1) % 7 + 1;
        }

        var indexOfMaxDayId = Array.IndexOf(dayIds, dayIds.Max());

        if (indexOfMaxDayId == dayIds.Length - 1)
        {
            result.Add(((int)weekType, dayIds));
        }
        else
        {
            result.Add(((int)weekType, dayIds.Take(indexOfMaxDayId).ToArray()));
            result.Add(((int)weekType, dayIds.TakeLast(dayIds.Length - indexOfMaxDayId - 1).ToArray()));
        }

        return result.ToArray();
    }
}