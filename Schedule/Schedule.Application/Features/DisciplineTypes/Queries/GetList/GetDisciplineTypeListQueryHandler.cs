using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineTypes.Queries.GetList;

public sealed class GetDisciplineTypeListQueryHandler
    : IRequestHandler<GetDisciplineTypeListQuery, PagedList<DisciplineTypeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineTypeListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<DisciplineTypeViewModel>> Handle(GetDisciplineTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var disciplines = await _context.Set<DisciplineType>()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        
        var viewModels = _mapper.Map<DisciplineTypeViewModel[]>(disciplines);
        var totalCount = await _context.Set<DisciplineType>().CountAsync(cancellationToken);

        return new PagedList<DisciplineTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}