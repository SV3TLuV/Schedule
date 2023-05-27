using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Notifications.CreateLessons;

public sealed class TimetableCreateLessonsNotificationHandler : INotificationHandler<TimetableCreateLessonsNotification>
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
    
    public async Task Handle(TimetableCreateLessonsNotification notification, CancellationToken cancellationToken)
    {
        var timetable = await _context.Set<Timetable>()
            .Include(e => e.Date)
            .Include(e => e.Group)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimetableId == notification.TimetableId, cancellationToken);

        if (timetable is null)
        {
            throw new NotFoundException(nameof(Template), notification.TimetableId);
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
            var lessonTemplate = template?.LessonTemplates
                .FirstOrDefault(e => e.Number == i);
            
            var command = new CreateLessonCommand
            {
                Number = i,
                TimetableId = notification.TimetableId,
                TimeId = lessonTemplate?.TimeId ?? null,
                DisciplineId = lessonTemplate?.DisciplineId ?? null,
                TeacherClassroomIds = lessonTemplate?.LessonTemplateTeacherClassrooms
                    .Select(e => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = e.TeacherId,
                        ClassroomId = e.ClassroomId
                    })
                    .ToArray() ?? Array.Empty<TeacherClassroomIdsViewModel>()
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}