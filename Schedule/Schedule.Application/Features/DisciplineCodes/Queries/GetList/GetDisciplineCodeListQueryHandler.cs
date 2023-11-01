using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Queries.GetList;

public sealed class GetDisciplineCodeListQueryHandler 
    : IRequestHandler<GetDisciplineCodeListQuery, PagedList<DisciplineCodeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineCodeListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<DisciplineCodeViewModel>> Handle(GetDisciplineCodeListQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _context.Set<DisciplineCode>()
            .OrderBy(e => e.Code)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e => e.Code.StartsWith(request.Search));

        var disciplineCodes = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<DisciplineCodeViewModel[]>(disciplineCodes);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<DisciplineCodeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}