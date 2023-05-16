using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetList;

public sealed class GetTeacherListQueryHandler
    : IRequestHandler<GetTeacherListQuery, PagedList<TeacherViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TeacherViewModel>> Handle(GetTeacherListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Teacher>()
            .Include(e => e.TeacherGroups)
            .ThenInclude(e => e.Group)
            .Include(e => e.TeacherDisciplines)
            .ThenInclude(e => e.Discipline)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };
        
        if (request.Search is not null)
        {
            query = query.Where(e =>
                e.Name.StartsWith(request.Search) ||
                e.Surname.StartsWith(request.Search) ||
                e.MiddleName.StartsWith(request.Search) ||
                e.Email.StartsWith(request.Search));
        }

        var teachers = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<TeacherViewModel[]>(teachers);
        var totalCount = await _context.Set<Teacher>().CountAsync(cancellationToken);

        return new PagedList<TeacherViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}