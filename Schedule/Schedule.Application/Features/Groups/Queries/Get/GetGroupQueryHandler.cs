using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.Get;

public sealed class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GroupViewModel> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .Include(e => e.SpecialityCode)
            .Include(e => e.Course)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        return _mapper.Map<GroupViewModel>(group);
    }
}