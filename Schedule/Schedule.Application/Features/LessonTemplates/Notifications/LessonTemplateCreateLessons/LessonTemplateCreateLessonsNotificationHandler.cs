using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateLessons;

public sealed class LessonTemplateCreateLessonsNotificationHandler
    : INotificationHandler<LessonTemplateCreateLessonsNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LessonTemplateCreateLessonsNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
        _dateInfoService = dateInfoService;
    }

    public async Task Handle(LessonTemplateCreateLessonsNotification notification,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .FirstAsync(e => e.LessonTemplateId == notification.LessonTemplateId, cancellationToken);

        var timetables = await _context.Set<Timetable>()
            .Include(e => e.Date)
            .Include(e => e.Lessons)
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.GroupId == lessonTemplate.Template.GroupId &&
                e.Date.DayId == lessonTemplate.Template.DayId &&
                e.Group.TermId == lessonTemplate.Template.TermId &&
                e.Date.WeekTypeId == lessonTemplate.Template.WeekTypeId &&
                e.Date.Value >= _dateInfoService.CurrentDateTime.Date)
            .ToListAsync(cancellationToken);

        foreach (var timetable in timetables)
        {
            var lesson = timetable.Lessons.FirstOrDefault(lesson => lesson.Number == lessonTemplate.Number);

            if (lesson is not null)
            {
                var command = _mapper.Map<UpdateLessonCommand>(lessonTemplate);
                command.TimetableId = timetable.TimetableId;
                command.Id = lesson.LessonId;
                await _mediator.Send(command, cancellationToken);
            }
            else
            {
                var command = _mapper.Map<CreateLessonCommand>(lessonTemplate);
                command.TimetableId = timetable.TimetableId;
                await _mediator.Send(command, cancellationToken);
            }
        }
    }
}