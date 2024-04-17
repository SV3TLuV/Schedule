using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler(
    IScheduleDbContext context,
    IMediator mediator,
    IMapper mapper,
    IDateInfoService dateInfoService)
    : IRequestHandler<CreateGroupCommand, int>
{
    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var searched = await context.Groups
            .AsNoTracking()
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e =>
                e.Number == request.Number &&
                e.SpecialityId == request.SpecialityId &&
                e.EnrollmentYear == request.EnrollmentYear, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Группа: {searched.Name}");
        
        var group = mapper.Map<Group>(request);
        group.TermId = group.CalculateTerm(dateInfoService);
        
        await context.Groups.AddAsync(group, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        await mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        return group.GroupId;
    }
}