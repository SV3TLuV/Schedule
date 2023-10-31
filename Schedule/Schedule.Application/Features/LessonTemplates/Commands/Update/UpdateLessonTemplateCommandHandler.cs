using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateForUnitedGroups;
using Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateLessons;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Update;

public sealed class UpdateLessonTemplateCommandHandler : IRequestHandler<UpdateLessonTemplateCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateLessonTemplateCommandHandler(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLessonTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var lessonTemplateDbo = await _context.Set<LessonTemplate>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.LessonTemplateId == request.Id, cancellationToken);

        if (lessonTemplateDbo is null)
            throw new NotFoundException(nameof(LessonTemplate), request.Id);

        await _context.Set<LessonTemplateTeacherClassroom>()
            .Where(entity =>
                entity.LessonTemplateId == lessonTemplateDbo.LessonTemplateId)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);

        var lessonTemplate = _mapper.Map<LessonTemplate>(request);

        foreach (var teacherClassroom in lessonTemplate.LessonTemplateTeacherClassrooms)
            teacherClassroom.LessonTemplateId = lessonTemplate.LessonTemplateId;

        _context.Set<LessonTemplate>().Update(lessonTemplate);
        _context.Set<LessonTemplateTeacherClassroom>()
            .AddRange(lessonTemplate.LessonTemplateTeacherClassrooms);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new LessonTemplateUpdateNotification(lessonTemplate.LessonTemplateId),
            cancellationToken);
        await _mediator.Publish(new LessonTemplateUpdateForUnitedGroupsNotification(lessonTemplate.LessonTemplateId),
            cancellationToken);
        return Unit.Value;
    }
}