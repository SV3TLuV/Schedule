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
    ITimetableRepository timetableRepository,
    IMapper mapper)
    : IRequestHandler<CreateGroupCommand, int>
{
    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var group = mapper.Map<Group>(request);
            var id = await groupRepository.CreateAsync(group, cancellationToken);

            await groupTransferRepository.CreateForGroup(id, cancellationToken);
            await timetableRepository.CreateForGroupAsync(id, cancellationToken);

            return id;
        }, cancellationToken);
    }
}