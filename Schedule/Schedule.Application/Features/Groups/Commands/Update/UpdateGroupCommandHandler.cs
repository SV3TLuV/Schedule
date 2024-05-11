using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler(
    IScheduleDbContext context,
    IGroupRepository groupRepository,
    IGroupTransferRepository groupTransferRepository,
    IMapper mapper)
    : IRequestHandler<UpdateGroupCommand, Unit>
{
    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var group = mapper.Map<Group>(request);

            await groupRepository.UpdateAsync(group, cancellationToken);

            await groupTransferRepository.DeleteByGroupId(group.GroupId, cancellationToken);
            await groupTransferRepository.CreateForGroup(group.GroupId, cancellationToken);
            return Unit.Value;
        }, cancellationToken);
    }
}