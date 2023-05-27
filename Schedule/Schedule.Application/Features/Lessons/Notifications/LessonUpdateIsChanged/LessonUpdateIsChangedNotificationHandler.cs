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
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .FirstAsync(e => e.LessonId == updateIsChangedNotification.Id, cancellationToken);
        
        var template = await _context.Set<LessonTemplate>()
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .FirstOrDefaultAsync(e => 
                e.Template.GroupId == lesson.Timetable.GroupId &&
                e.Template.DayId == lesson.Timetable.Date.DayId &&
                e.Template.WeekTypeId == lesson.Timetable.Date.WeekTypeId &&
                e.Template.TermId == lesson.Timetable.Group.TermId, cancellationToken);

        lesson.IsChanged = !lesson.Equals(template);
        await _context.SaveChangesAsync(cancellationToken);
    }
}