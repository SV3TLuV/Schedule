using AutoMapper;
using MediatR;
using Schedule.Application.Features.Lessons.Notifications.LessonCreateForUnitedGroups;
using Schedule.Application.Features.Lessons.Notifications.LessonUpdateIsChanged;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Create;

public sealed class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

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

        foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            teacherClassroom.LessonId = lesson.LessonId;

        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new LessonUpdateIsChangedNotification(lesson.LessonId), cancellationToken);
        await _mediator.Publish(new LessonCreateForUnitedGroupsNotification(lesson.LessonId), cancellationToken);
        return lesson.LessonId;
    }
}