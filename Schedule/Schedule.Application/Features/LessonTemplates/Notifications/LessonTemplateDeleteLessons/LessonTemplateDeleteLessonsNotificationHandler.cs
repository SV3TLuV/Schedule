using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Lessons.Commands.Delete;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateDeleteLessons;

public sealed class LessonTemplateDeleteLessonsNotificationHandler
    : INotificationHandler<LessonTemplateDeleteLessonsNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMediator _mediator;

    public LessonTemplateDeleteLessonsNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _mediator = mediator;
        _dateInfoService = dateInfoService;
    }

    public async Task Handle(LessonTemplateDeleteLessonsNotification notification,
        CancellationToken cancellationToken)
    {
        var lessonIds = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.Number == notification.LessonTemplate.Number &&
                e.Timetable.GroupId == notification.LessonTemplate.Template.GroupId &&
                e.Timetable.Group.TermId == notification.LessonTemplate.Template.TermId &&
                e.Timetable.Date.DayId == notification.LessonTemplate.Template.DayId &&
                e.Timetable.Date.Value >= _dateInfoService.CurrentDateTime.Date &&
                e.Timetable.Date.WeekTypeId == notification.LessonTemplate.Template.WeekTypeId)
            .Select(e => e.LessonId)
            .ToListAsync(cancellationToken);

        foreach (var lessonId in lessonIds)
        {
            var command = new DeleteLessonCommand(lessonId);
            await _mediator.Send(command, cancellationToken);
        }
    }
}