using AutoMapper;
using MediatR;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTemplates;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateGroupCommandHandler(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<Group>(request);
        await _context.Set<Group>().AddAsync(group, cancellationToken);

        foreach (var groupGroup in group.GroupGroups)
            groupGroup.GroupId = group.GroupId;
        
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupCreateTemplatesNotification(group.GroupId), cancellationToken);
        await _mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        return group.GroupId;
    }
}