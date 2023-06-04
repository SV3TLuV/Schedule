using AutoMapper;
using MediatR;
using Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Commands.Create;

public sealed class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateTemplateCommandHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = _mapper.Map<Template>(request);
        await _context.Set<Template>().AddAsync(template, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new TemplateCreateLessonTemplatesNotification(template.TemplateId), cancellationToken);
        return template.TemplateId;
    }
}