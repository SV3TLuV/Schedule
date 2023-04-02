using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed class GetSpecialityCodeListQueryHandler 
    : IRequestHandler<GetSpecialityCodeListQuery, SpecialityCodeViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityCodeListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<SpecialityCodeViewModel[]> Handle(GetSpecialityCodeListQuery request,
        CancellationToken cancellationToken)
    {
        var specialityCodes = await _context.SpecialityCodes
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.SpecialityCodeId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<SpecialityCodeViewModel[]>(specialityCodes);
    }
}