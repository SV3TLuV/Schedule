using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupUniteUpdateLessons;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Unite;

public sealed class UniteGroupsCommandHandler : IRequestHandler<UniteGroupsCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public UniteGroupsCommandHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(UniteGroupsCommand request,
        CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.GroupId, cancellationToken);
        
        if (group is null)
            throw new NotFoundException(nameof(Group), request.GroupId);
        
        var group2 = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.GroupId, cancellationToken);
        
        if (group2 is null)
            throw new NotFoundException(nameof(Group), request.GroupId2);

        await _context.Set<GroupGroup>().AddAsync(new GroupGroup
        {
            GroupId = request.GroupId,
            GroupId2 = request.GroupId2,
        }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupUniteUpdateLessonsNotification(request.GroupId, request.GroupId2),
            cancellationToken);
    }
}