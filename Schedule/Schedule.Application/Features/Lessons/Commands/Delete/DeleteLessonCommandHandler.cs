using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Notifications.Deleted;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Delete;

public sealed class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DeleteLessonCommandHandler(IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(DeleteLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.LessonId == request.Id, cancellationToken);

        if (lesson is null)
            throw new NotFoundException(nameof(Lesson), request.Id);

        _context.Set<Lesson>().Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new DeletedLessonNotification(lesson.LessonId), cancellationToken);
    }
}