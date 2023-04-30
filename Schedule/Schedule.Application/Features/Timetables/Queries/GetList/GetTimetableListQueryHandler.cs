using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public sealed class GetTimetableListQueryHandler 
    : IRequestHandler<GetTimetableListQuery, PagedList<TimetableViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimetableListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<TimetableViewModel>> Handle(GetTimetableListQuery request,
        CancellationToken cancellationToken)
    {
        var timetables = await _context.Set<Timetable>()
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Course)
            .Include(e => e.Date)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Time)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var totalCount = await _context.Set<Timetable>().CountAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TimetableViewModel>>(timetables);
        
        foreach (var viewModel in viewModels)
        {
            if (viewModel.Groups.Count <= 1)
                continue;

            for (var i = 1; i < viewModel.Groups.Count; i++)
            {
                var group = viewModel.Groups.ElementAt(i);
                var timetable = viewModels.First(v =>
                    v.Groups.First().Id == group.Id);
                viewModels.Remove(timetable);
            }
            
            viewModel.Groups = viewModel.Groups
                .OrderBy(e => e.Speciality.Code)
                .ToArray();
        }
        
        return new PagedList<TimetableViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}