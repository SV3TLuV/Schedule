using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateLessons;

public sealed class LessonTemplateUpdateNotificationHandler : INotificationHandler<LessonTemplateUpdateNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IDateInfoService _dateInfoService;

    public LessonTemplateUpdateNotificationHandler(
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

    public async Task Handle(LessonTemplateUpdateNotification notification,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .FirstAsync(e => e.LessonTemplateId == notification.LessonTemplateId, cancellationToken);
        
        var lessons = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .Where(e => 
                e.Number == lessonTemplate.Number &&
                e.Timetable.GroupId == lessonTemplate.Template.GroupId &&
                e.Timetable.Group.TermId == lessonTemplate.Template.TermId &&
                e.Timetable.Date.DayId == lessonTemplate.Template.DayId &&
                e.Timetable.Date.Value >= _dateInfoService.CurrentDateTime.Date &&
                e.Timetable.Date.WeekTypeId == lessonTemplate.Template.WeekTypeId)
            .ToListAsync(cancellationToken);

        foreach (var lesson in lessons)
        {
            var command = _mapper.Map<UpdateLessonCommand>(lessonTemplate);
            command.Id = lesson.LessonId;
            command.TimetableId = lesson.TimetableId;
            await _mediator.Send(command, cancellationToken);
        }
    }
}