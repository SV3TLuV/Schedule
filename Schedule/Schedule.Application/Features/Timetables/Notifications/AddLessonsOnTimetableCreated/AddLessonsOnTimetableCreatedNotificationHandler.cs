using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Templates.Notifications.AddLessonsOnTemplateCreated;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Notifications.AddLessonsOnTimetableCreated;

public sealed class AddLessonsOnTimetableCreatedNotificationHandler : INotificationHandler<AddLessonsOnTemplateCreated>
{
    private readonly IScheduleDbContext _context;

    public AddLessonsOnTimetableCreatedNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(AddLessonsOnTemplateCreated onTemplateCreated, CancellationToken cancellationToken)
    {
        var timetable = await _context.Set<Timetable>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimetableId == onTemplateCreated.Id, cancellationToken);

        if (timetable is null)
            throw new NotFoundException(nameof(Template), onTemplateCreated.Id);

        var lessons = new List<Lesson>();

        for (var i = 1; i <= 4; i++)
        {
            lessons.Add(new Lesson
            {
                Number = i,
                TimetableId = onTemplateCreated.Id,
            });
        }
        
        await _context.Set<Lesson>().AddRangeAsync(lessons, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}