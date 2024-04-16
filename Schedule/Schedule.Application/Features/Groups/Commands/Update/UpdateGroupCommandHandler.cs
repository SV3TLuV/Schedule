using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IMediator mediator,
    IDateInfoService dateInfoService)
    : IRequestHandler<UpdateGroupCommand, Unit>
{
    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupDbo = await context.Groups
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (groupDbo is null)
            throw new NotFoundException(nameof(Group), request.Id);
        
        var group = mapper.Map<Group>(request);
        
        group.TermId = group.CalculateTerm(dateInfoService);
        
        context.Groups.Update(group);
        await context.SaveChangesAsync(cancellationToken);
        await mediator.Publish(new GroupDeleteTransfersNotification(group.GroupId), cancellationToken);
        await mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}