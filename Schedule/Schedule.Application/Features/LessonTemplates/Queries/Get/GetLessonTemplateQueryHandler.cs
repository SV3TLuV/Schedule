using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Queries.Get;

public sealed class GetLessonTemplateQueryHandler
    : IRequestHandler<GetLessonTemplateQuery, LessonTemplateViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetLessonTemplateQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LessonTemplateViewModel> Handle(GetLessonTemplateQuery request,
        CancellationToken cancellationToken)
    {
        var template = await _context.Set<LessonTemplate>()
            .Include(e => e.Discipline)
            .ThenInclude(e => e.Name)
            .Include(e => e.Discipline)
            .ThenInclude(e => e.Code)
            .Include(e => e.Time)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.LessonTemplateId == request.Id,
                cancellationToken);

        if (template is null)
            throw new NotFoundException(nameof(LessonTemplate), request.Id);

        return _mapper.Map<LessonTemplateViewModel>(template);
    }
}