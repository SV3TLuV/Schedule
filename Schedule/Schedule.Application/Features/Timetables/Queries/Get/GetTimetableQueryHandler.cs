using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.Get;

public sealed class GetTimetableQueryHandler : IRequestHandler<GetTimetableQuery, TimetableViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimetableQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TimetableViewModel> Handle(GetTimetableQuery request, CancellationToken cancellationToken)
    {
        var timetable = await _context.Set<Timetable>()
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
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Date)
            .ThenInclude(e => e.Day)
            .Include(e => e.Date)
            .ThenInclude(e => e.WeekType)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Name)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.Code)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .ThenInclude(e => e.DisciplineType)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Time)
            .ThenInclude(e => e.Type)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.TimetableId == request.Id, cancellationToken);

        if (timetable is null)
            throw new NotFoundException(nameof(Timetable), request.Id);

        var viewModel = _mapper.Map<TimetableViewModel>(timetable);

        viewModel.Groups = viewModel.Groups
            .OrderBy(e => e.Speciality.Code)
            .ToArray();

        return viewModel;
    }
}