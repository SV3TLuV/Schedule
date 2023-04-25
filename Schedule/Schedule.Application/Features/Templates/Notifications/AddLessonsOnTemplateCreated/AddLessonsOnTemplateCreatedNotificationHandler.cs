using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Notifications.AddLessonsOnTemplateCreated;

public sealed class AddLessonsOnTemplateCreatedNotificationHandler : INotificationHandler<AddLessonsOnTemplateCreated>
{
    private readonly IScheduleDbContext _context;

    public AddLessonsOnTemplateCreatedNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(AddLessonsOnTemplateCreated onTemplateCreated, CancellationToken cancellationToken)
    {
        var template = await _context.Set<Template>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TemplateId == onTemplateCreated.Id, cancellationToken);

        if (template is null)
            throw new NotFoundException(nameof(Template), onTemplateCreated.Id);

        var lessons = new List<LessonTemplate>();

        for (var i = 1; i <= 4; i++)
        {
            lessons.Add(new LessonTemplate
            {
                Number = i,
                TemplateId = onTemplateCreated.Id,
            });
        }
        
        await _context.Set<LessonTemplate>().AddRangeAsync(lessons, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}