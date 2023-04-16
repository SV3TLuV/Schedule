using AutoMapper;
using MediatR;
using Schedule.Application.Features.Lessons.Notifications.Created;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Create;

public sealed class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateLessonCommandHandler(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = _mapper.Map<Lesson>(request);
        await _context.Set<Lesson>().AddAsync(lesson, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new CreatedLessonNotification(lesson.LessonId), cancellationToken);
        return lesson.LessonId;
    }
}