using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTemplates;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTimetables;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Application.Features.Groups.Notifications.GroupUpdateForMergedGroups;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMediator _mediator;

    public CreateGroupCommandHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
        _dateInfoService = dateInfoService;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var searched = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e =>
                e.Number == request.Number &&
                e.SpecialityId == request.SpecialityId &&
                e.EnrollmentYear == request.EnrollmentYear, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Группа: {searched.Name}");
        
        var group = _mapper.Map<Group>(request);
        
        if (group.GroupGroups.Any())
        {
            var specialityIsEqual = await _context.Set<Group>()
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
        
        await _context.Set<Group>().AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupCreateTemplatesNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupCreateTimetablesNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupUpdateForMergedGroupsNotification(group.GroupId), cancellationToken);
        return group.GroupId;
    }
}