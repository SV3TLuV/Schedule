using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Notifications.CreateLessons;

public sealed class TimetableCreateLessonsNotificationHandler : INotificationHandler<TemplateCreateLessonTemplatesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public TimetableCreateLessonsNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(TemplateCreateLessonTemplatesNotification notification, CancellationToken cancellationToken)
    {
        var timetable = await _context.Set<Timetable>()
            .Include(e => e.Date)
            .Include(e => e.Group)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimetableId == notification.TemplateId, cancellationToken);

        if (timetable is null)
        {
            throw new NotFoundException(nameof(Template), notification.TemplateId);
        }

        var template = await _context.Set<Template>()
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => 
                e.DayId == timetable.Date.DayId &&
                e.WeekTypeId == timetable.Date.WeekTypeId &&
                e.GroupId == timetable.GroupId &&
                e.TermId == timetable.Group.TermId, cancellationToken);

        for (var i = 1; i <= 4; i++)
        {
            var command = new CreateLessonCommand
            {
                Number = i,
                TimetableId = notification.TemplateId,
                /*TimeId = lessonTemplate?.TimeId ?? null,
                DisciplineId = lessonTemplate?.DisciplineId ?? null,
                TeacherClassroomIds = null*/
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}