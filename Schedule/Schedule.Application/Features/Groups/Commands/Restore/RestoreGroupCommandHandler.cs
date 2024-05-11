using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public sealed class RestoreGroupCommandHandler(
    IScheduleDbContext context,
    IGroupRepository groupRepository,
    IGroupTransferRepository groupTransferRepository) : IRequestHandler<RestoreGroupCommand, Unit>
{
    public async Task<Unit> Handle(RestoreGroupCommand request, CancellationToken cancellationToken)
    {
        return await context.WithTransactionAsync(async () =>
        {
            await groupRepository.RestoreAsync(request.Id, cancellationToken);
            await groupTransferRepository.CreateForGroup(request.Id, cancellationToken);
            return Unit.Value;
        }, cancellationToken);
    }
}