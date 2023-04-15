using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
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
        var specialityCodes = await _context.Set<SpecialityCode>()
            .Include(e => e.Disciplines)
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.SpecialityCodeId)
            .ToListAsync(cancellationToken);
        
        var viewModels = _mapper.Map<SpecialityCodeViewModel[]>(specialityCodes);
        var totalCount = await _context.Set<SpecialityCodeViewModel>().CountAsync(cancellationToken);

        return new PagedList<SpecialityCodeViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}