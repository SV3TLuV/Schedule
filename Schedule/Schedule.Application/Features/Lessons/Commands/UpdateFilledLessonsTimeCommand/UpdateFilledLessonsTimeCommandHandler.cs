using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.UpdateFilledLessonsTimeCommand;

public sealed class UpdateFilledLessonsTimeCommandHandler : IRequestHandler<UpdateFilledLessonsTimeCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateFilledLessonsTimeCommandHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateFilledLessonsTimeCommand request,
        CancellationToken cancellationToken)
    {
        var lessonQuery = _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Timetable)
            .Include(e => e.Time)
            .Include(e => e.LessonTeacherClassrooms)
            .Where(e =>
                e.Timetable.DateId == request.DateId &&
                e.DisciplineId != null &&
                e.TimeId != null &&
                e.LessonTeacherClassrooms.Count != 0 &&
                e.Time!.TypeId != request.TimeTypeId);

        var timeQuery = _context.Set<Time>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.TypeId == request.TimeTypeId);

        if (request.PairNumbers is not null)
        {
            lessonQuery = lessonQuery.Where(e => request.PairNumbers.Contains(e.Number));
            timeQuery = timeQuery.Where(e => request.PairNumbers.Contains(e.LessonNumber));
        }
        
        var lessons = await lessonQuery.ToListAsync(cancellationToken);
        var times = await timeQuery.ToListAsync(cancellationToken);

        foreach (var lesson in lessons)
        {
            var time = times.FirstOrDefault(e => e.LessonNumber == lesson.Number);

            if (time is null)
            {
                continue;
            }
            
            var command = _mapper.Map<UpdateLessonCommand>(lesson);
            command.TimeId = time.TimeId;
            await _mediator.Send(command, cancellationToken);
        }
    }
}