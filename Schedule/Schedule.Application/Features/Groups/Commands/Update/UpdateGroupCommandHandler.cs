using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;
using Schedule.Application.Features.Groups.Notifications.GroupUpdateForMergedGroups;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IDateInfoService _dateInfoService;

    public UpdateGroupCommandHandler(
        IScheduleDbContext context, 
        IMapper mapper,
        IMediator mediator,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
        _dateInfoService = dateInfoService;
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

        if (group.GroupGroups.Any())
        {
            var specialityIsEqual = await _context.Set<Group>()
                .AsNoTracking()
                .Where(e => request.MergedGroupIds!.Contains(e.GroupId))
                .AllAsync(e => e.SpecialityId == group.SpecialityId,cancellationToken);

            if (!specialityIsEqual)
            {
                group.GroupGroups.Clear();
            }

            var hasUnitedGroups = await _context.Set<GroupGroup>()
                .AsNoTracking()
                .Where(e => request.MergedGroupIds!.Contains(e.GroupId))
                .AnyAsync(cancellationToken);

            if (hasUnitedGroups)
            {
                throw new ScheduleException("Объединяемая группа уже объединена с другой!");
            }
        }
        
        group.TermId = _dateInfoService.GetGroupTerm(group.EnrollmentYear, group.IsAfterEleven);
        
        _context.Set<Group>().Update(group);
        await _context.Set<GroupGroup>().AddRangeAsync(group.GroupGroups, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupDeleteTransfersNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupUpdateForMergedGroupsNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}