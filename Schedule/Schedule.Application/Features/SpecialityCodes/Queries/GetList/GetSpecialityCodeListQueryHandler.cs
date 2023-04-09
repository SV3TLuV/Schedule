using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed class GetSpecialityCodeListQueryHandler
    : IRequestHandler<GetSpecialityCodeListQuery, SpecialityCodeViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityCodeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SpecialityCodeViewModel[]> Handle(GetSpecialityCodeListQuery request,
        CancellationToken cancellationToken)
    {
        var specialityCodes = await _context.Set<SpecialityCode>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.SpecialityCodeId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<SpecialityCodeViewModel[]>(specialityCodes);
    }
}