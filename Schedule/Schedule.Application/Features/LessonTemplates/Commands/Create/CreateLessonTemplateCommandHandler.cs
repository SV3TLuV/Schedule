using AutoMapper;
using MediatR;
using Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateLessons;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Create;

public sealed class CreateLessonTemplateCommandHandler
    : IRequestHandler<CreateLessonTemplateCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateLessonTemplateCommandHandler(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLessonTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = _mapper.Map<LessonTemplate>(request);
        await _context.Set<LessonTemplate>().AddAsync(lessonTemplate, cancellationToken);

        foreach (var teacherClassroom in lessonTemplate.LessonTemplateTeacherClassrooms)
            teacherClassroom.LessonTemplateId = lessonTemplate.LessonTemplateId;

        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new LessonTemplateCreateLessonsNotification(lessonTemplate.LessonTemplateId),
            cancellationToken);
        return lessonTemplate.LessonTemplateId;
    }
}