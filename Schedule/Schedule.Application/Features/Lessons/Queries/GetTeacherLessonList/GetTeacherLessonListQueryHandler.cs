using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Lessons.Queries.GetTeacherLessonList;

public sealed class GetTeacherLessonListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IDateInfoService dateInfoService) : IRequestHandler<GetTeacherLessonListQuery, LessonViewModel[]>
{
    public async Task<LessonViewModel[]> Handle(GetTeacherLessonListQuery request,
        CancellationToken cancellationToken)
    {
        var dayId = dateInfoService.GetDayId(request.Date);
        var weekTypeId = (int)dateInfoService.GetWeekType(request.Date);

        return await context.Lessons
            .AsNoTracking()
            .Include(e => e.Timetable)
            .Include(e => e.Discipline)
            .ThenInclude(e => e!.Code)
            .Include(e => e.Discipline)
            .ThenInclude(e => e!.Name)
            .Include(e => e.Discipline)
            .ThenInclude(e => e!.Type)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .ThenInclude(e => e.Account)
            .Include(e => e.LessonChanges!
                .Where(lessonChange => lessonChange.Date == request.Date))
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Code)
            .Include(e => e.LessonChanges!
                .Where(lessonChange => lessonChange.Date == request.Date))
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Name)
            .Include(e => e.LessonChanges!
                .Where(lessonChange => lessonChange.Date == request.Date))
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Type)
            .Include(e => e.LessonChanges!
                .Where(lessonChange => lessonChange.Date == request.Date))
            .ThenInclude(e => e.LessonChangeTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonChanges!
                .Where(lessonChange => lessonChange.Date == request.Date))
            .ThenInclude(e => e.LessonChangeTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .ThenInclude(e => e.Account)
            .Where(e =>
                e.Timetable.DayId == dayId &&
                e.Timetable.WeekTypeId == weekTypeId &&
                e.Timetable.Ended == null &&
                e.LessonTeacherClassrooms
                    .Select(teacherClassroom => teacherClassroom.TeacherId)
                    .Contains(request.TeacherId))
            .ProjectTo<LessonViewModel>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}