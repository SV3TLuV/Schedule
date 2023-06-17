using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupUpdateForMergedGroups;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateGroupCommandHandler(
        IScheduleDbContext context, 
        IMapper mapper,
        IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupDbo = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (groupDbo is null)
            throw new NotFoundException(nameof(Group), request.Id);
        
        var group = _mapper.Map<Group>(request);

        await _context.Set<GroupGroup>()
            .Where(e =>
                e.GroupId == request.Id ||
                e.GroupId2 == request.Id)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);
        
        _context.Set<Group>().Update(group);
        await _context.Set<GroupGroup>().AddRangeAsync(group.GroupGroups, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupUpdateForMergedGroupsNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}