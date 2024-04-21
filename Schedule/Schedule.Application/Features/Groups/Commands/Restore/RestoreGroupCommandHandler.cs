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
        await context.WithTransactionAsync(async () =>
        {
            groupRepository.UseContext(context);
            groupTransferRepository.UseContext(context);

            await groupRepository.RestoreAsync(request.Id, cancellationToken);
            await groupTransferRepository.CreateForGroup(request.Id, cancellationToken);
        }, cancellationToken);

        return Unit.Value;
    }
}