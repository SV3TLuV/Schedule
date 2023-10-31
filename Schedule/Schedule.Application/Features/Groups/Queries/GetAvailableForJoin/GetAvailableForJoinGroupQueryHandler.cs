using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetAvailableForJoin;

public sealed class GetAvailableForJoinGroupQueryHandler 
    : IRequestHandler<GetAvailableForJoinGroupQuery, GroupViewModel[]>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetAvailableForJoinGroupQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<GroupViewModel[]> Handle(GetAvailableForJoinGroupQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Group>()
            .Where(e => !e.IsDeleted)
            .Where(e => e.TermId == request.TermId)
            .Where(e => e.SpecialityId == request.SpecialityId);

        if (request.GroupId is not null)
        {
            query = query.Where(e => e.GroupId != request.GroupId);
        }

        var availableGroups = await query
            .Include(e => e.Speciality)
            .Include(e => e.Term)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<GroupViewModel[]>(availableGroups);
    }
}