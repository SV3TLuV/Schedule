using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed class GetDisciplineListQueryHandler
    : IRequestHandler<GetDisciplineListQuery, PagedList<DisciplineViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<DisciplineViewModel>> Handle(GetDisciplineListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Discipline>()
            .Include(e => e.SpecialityCode)
            .Include(e => e.Term)
            .ThenInclude(e => e.Course)
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Code)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        var disciplines = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<DisciplineViewModel[]>(disciplines);
        var totalCount = await _context.Set<Discipline>().CountAsync(cancellationToken);

        return new PagedList<DisciplineViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}