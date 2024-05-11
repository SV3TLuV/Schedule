using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Commands.Delete;

public sealed class DeleteGroupCommandHandler(
    IScheduleDbContext context,
    IGroupRepository groupRepository,
    IGroupTransferRepository groupTransferRepository) : IRequestHandler<DeleteGroupCommand, Unit>
{
    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        return await context.WithTransactionAsync(async () =>
        {
            await groupRepository.DeleteAsync(request.Id, cancellationToken);
            await groupTransferRepository.DeleteByGroupId(request.Id, cancellationToken);

            return Unit.Value;
        }, cancellationToken);
    }
}