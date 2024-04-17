using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Queries.Get;

public sealed class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, LessonViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetLessonQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LessonViewModel> Handle(GetLessonQuery request,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .Include(e => e.Discipline)
            .ThenInclude(e => e.Name)
            .Include(e => e.Discipline)
            .ThenInclude(e => e.Code)
            .Include(e => e.Time)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Include(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.LessonId == request.Id, cancellationToken);

        if (lesson is null)
            throw new NotFoundException(nameof(Lesson), request.Id);

        return _mapper.Map<LessonViewModel>(lesson);
    }
}