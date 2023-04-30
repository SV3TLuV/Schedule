using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Notifications.CreatedOrUpdated;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateLessonCommandHandler(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lessonDbo = await _context.Set<Lesson>()
            .FirstOrDefaultAsync(e => e.LessonId == request.Id, cancellationToken);

        if (lessonDbo is null)
            throw new NotFoundException(nameof(Lesson), request.Id);

        await _context.Set<LessonTeacherClassroom>()
            .Where(entity => entity.LessonId == lessonDbo.LessonId)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);
        
        var lesson = _mapper.Map<Lesson>(request);

        foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            teacherClassroom.LessonId = lesson.LessonId;

        _context.Set<Lesson>().Update(lesson);
        await _context.Set<LessonTeacherClassroom>()
            .AddRangeAsync(lesson.LessonTeacherClassrooms, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new CreatedOrUpdatedLessonNotification(lesson.LessonId), cancellationToken);
    }
}