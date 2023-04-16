using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed class GetSpecialityCodeListQueryHandler
    : IRequestHandler<GetSpecialityCodeListQuery, PagedList<SpecialityCodeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityCodeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<SpecialityCodeViewModel>> Handle(GetSpecialityCodeListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<SpecialityCode>()
            .Include(e => e.Disciplines)
            .OrderBy(e => e.SpecialityCodeId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        var specialityCodes = await query.ToArrayAsync(cancellationToken);
        var viewModels = _mapper.Map<SpecialityCodeViewModel[]>(specialityCodes);
        var totalCount = await _context.Set<SpecialityCodeViewModel>().CountAsync(cancellationToken);

        return new PagedList<SpecialityCodeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}