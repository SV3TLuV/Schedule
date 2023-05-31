using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateLessons;

public sealed class LessonTemplateCreateLessonsNotificationHandler 
    : INotificationHandler<LessonTemplateCreateLessonsNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IDateInfoService _dateInfoService;

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

        var timetableIds = await _context.Set<Timetable>()
            .Include(e => e.Date)
            .AsNoTrackingWithIdentityResolution()
            .Where(e => 
                e.GroupId == lessonTemplate.Template.GroupId &&
                e.Date.DayId == lessonTemplate.Template.DayId &&
                e.Group.TermId == lessonTemplate.Template.TermId &&
                e.Date.WeekTypeId == lessonTemplate.Template.WeekTypeId &&
                e.Date.Value >= _dateInfoService.CurrentDateTime.Date)
            .Select(e => e.TimetableId)
            .ToListAsync(cancellationToken);

        foreach (var timetableId in timetableIds)
        {
            var command = _mapper.Map<CreateLessonCommand>(lessonTemplate);
            command.TimetableId = timetableId;
            await _mediator.Send(command, cancellationToken);
        }
    }
}