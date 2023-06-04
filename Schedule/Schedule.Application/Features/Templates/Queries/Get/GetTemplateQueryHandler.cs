using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Queries.Get;

public sealed class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, TemplateViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTemplateQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TemplateViewModel> Handle(GetTemplateQuery request,
        CancellationToken cancellationToken)
    {
        var template = await _context.Set<Template>()
            .Include(e => e.Day)
            .Include(e => e.Term)
            .Include(e => e.WeekType)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Time)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TemplateId == request.Id, cancellationToken);

        if (template is null)
            throw new NotFoundException(nameof(Template), request.Id);

        var viewModel = _mapper.Map<TemplateViewModel>(template);

        viewModel.Groups = viewModel.Groups
            .OrderBy(e => e.Speciality.Code)
            .ToArray();

        return viewModel;
    }
}