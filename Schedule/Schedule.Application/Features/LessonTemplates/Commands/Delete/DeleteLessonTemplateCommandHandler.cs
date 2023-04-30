using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.LessonTemplates.Notifications.Deleted;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Delete;

public sealed class DeleteLessonTemplateCommandHandler : IRequestHandler<DeleteLessonTemplateCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DeleteLessonTemplateCommandHandler(IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(DeleteLessonTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = await _context.Set<LessonTemplate>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.LessonTemplateId == request.Id, cancellationToken);

        if (lessonTemplate is null)
            throw new NotFoundException(nameof(LessonTemplate), request.Id);

        _context.Set<LessonTemplate>().Remove(lessonTemplate);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new DeletedLessonTemplateNotification(lessonTemplate), cancellationToken);
    }
}