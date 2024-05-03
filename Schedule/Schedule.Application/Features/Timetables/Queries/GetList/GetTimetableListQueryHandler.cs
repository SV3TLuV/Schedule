using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public sealed class GetTimetableListQueryHandler(
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    IMapper mapper) : IRequestHandler<GetTimetableListQuery, ICollection<TimetableViewModel>>
{
    public async Task<ICollection<TimetableViewModel>> Handle(GetTimetableListQuery request,
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
            .Where(e =>
                e.Ended == null &&
                !e.Group.IsDeleted)
            .AsSplitQuery()
            .AsNoTracking();

        if (request.Date is null)
        {
            query = query
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
                .ThenInclude(e => e.Account);
        }
        else
        {
            query = query
                .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date == request.Date))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Name)
                .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date == request.Date))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Code)
                .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date == request.Date))
                .ThenInclude(e => e.Discipline)
                .ThenInclude(e => e.Type)
                .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date == request.Date))
                .ThenInclude(e => e.LessonChangeTeacherClassrooms)
                .ThenInclude(e => e.Classroom)
                .Include(e => e.Lessons)
                .ThenInclude(e => e.LessonChanges!
                    .Where(e => e.Date == request.Date))
                .ThenInclude(e => e.LessonChangeTeacherClassrooms)
                .ThenInclude(e => e.Teacher)
                .ThenInclude(e => e.Account);
        }

        if (request.GroupId is not null)
        {
            query = query.Where(e => e.GroupId == request.GroupId);
        }

        if (request.WeekTypeId is not null)
        {
            query = query.Where(e => e.WeekTypeId == (int)request.WeekTypeId);
        }

        if (request.DayId is not null)
        {
            query = query.Where(e => e.DayId == request.DayId);
        }

        return await query
            .ProjectTo<TimetableViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}