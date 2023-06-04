using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.LessonUpdateIsChanged;

public sealed class LessonUpdateIsChangedNotificationHandler : INotificationHandler<LessonUpdateIsChangedNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonUpdateIsChangedNotificationHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonUpdateIsChangedNotification updateIsChangedNotification,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .AsSplitQuery()
            .FirstAsync(e => e.LessonId == updateIsChangedNotification.Id, cancellationToken);

        var template = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e =>
                e.Number == lesson.Number &&
                e.Template.GroupId == lesson.Timetable.GroupId &&
                e.Template.DayId == lesson.Timetable.Date.DayId &&
                e.Template.WeekTypeId == lesson.Timetable.Date.WeekTypeId &&
                e.Template.TermId == lesson.Timetable.Group.TermId, cancellationToken);

        lesson.IsChanged = !lesson.Equals(template);
        _context.Set<Lesson>().Update(lesson);
        await _context.SaveChangesAsync(cancellationToken);
    }
}