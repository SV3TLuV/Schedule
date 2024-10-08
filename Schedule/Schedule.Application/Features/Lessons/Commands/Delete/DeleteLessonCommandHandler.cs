﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Notifications.LessonDeleteForUnitedGroups;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Delete;

public sealed class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DeleteLessonCommandHandler(IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(DeleteLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .FirstOrDefaultAsync(e => e.LessonId == request.Id, cancellationToken);

        if (lesson is null)
            throw new NotFoundException(nameof(Lesson), request.Id);

        await _context.Set<LessonTeacherClassroom>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.LessonId == lesson.LessonId)
            .ExecuteDeleteAsync(cancellationToken);
        
        _context.Set<Lesson>().Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new LessonDeleteForUnitedGroupsNotification(lesson), cancellationToken);
        return Unit.Value;
    }
}