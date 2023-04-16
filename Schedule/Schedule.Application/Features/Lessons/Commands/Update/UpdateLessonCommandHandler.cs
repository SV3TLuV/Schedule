using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Notifications.Updated;
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

        var lesson = _mapper.Map<Lesson>(request);
        _context.Set<Lesson>().Update(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new UpdatedLessonNotification(lesson.LessonId), cancellationToken);
    }
}