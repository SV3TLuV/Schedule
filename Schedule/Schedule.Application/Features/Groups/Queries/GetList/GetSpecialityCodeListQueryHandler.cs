using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed class GetSpecialityCodeListQueryHandler
    : IRequestHandler<GetGroupListQuery, GroupViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityCodeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GroupViewModel[]> Handle(GetGroupListQuery request,
        CancellationToken cancellationToken)
    {
        var groups = await _context.Set<Group>()
            .Include(e => e.SpecialityCode)
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.SpecialityCode.Code)
            .ToListAsync(cancellationToken);
        return _mapper.Map<GroupViewModel[]>(groups);
    }
}