using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Groups.Queries.Get;

public sealed class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<GroupViewModel> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .Include(e => e.SpecialityCode)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        return _mapper.Map<GroupViewModel>(group);
    }
}