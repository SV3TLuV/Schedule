using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Queries.GetList;

public sealed class GetDisciplineNameQueryListHandler 
    : IRequestHandler<GetDisciplineNameQueryList, PagedList<DisciplineNameViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineNameQueryListHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<DisciplineNameViewModel>> Handle(GetDisciplineNameQueryList request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<DisciplineName>()
            .OrderBy(e => e.Name)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e => e.Name.StartsWith(request.Search));

        var disciplineNames = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<DisciplineNameViewModel[]>(disciplineNames);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DisciplineNameViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}