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
        await context.WithTransactionAsync(async () =>
        {
            groupRepository.UseContext(context);
            groupTransferRepository.UseContext(context);

            await groupRepository.DeleteAsync(request.Id, cancellationToken);
            await groupTransferRepository.DeleteByGroupId(request.Id, cancellationToken);
        }, cancellationToken);

        return Unit.Value;
    }
}