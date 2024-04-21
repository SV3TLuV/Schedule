using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler(
    IScheduleDbContext context,
    IGroupRepository groupRepository,
    IGroupTransferRepository groupTransferRepository,
    IMapper mapper)
    : IRequestHandler<CreateGroupCommand, int>
{
    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var id = default(int);

        await context.WithTransactionAsync(async () =>
        {
            groupRepository.UseContext(context);
            groupTransferRepository.UseContext(context);

            var group = mapper.Map<Group>(request);
            id = await groupRepository.CreateAsync(group, cancellationToken);

            await groupTransferRepository.CreateForGroup(id, cancellationToken);
        }, cancellationToken);

        return id;
    }
}