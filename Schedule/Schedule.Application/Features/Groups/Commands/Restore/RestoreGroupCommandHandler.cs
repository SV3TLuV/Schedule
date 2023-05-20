using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public sealed class RestoreGroupCommandHandler : IRequestHandler<RestoreGroupCommand>
{
    private readonly IScheduleDbContext _context;

    public RestoreGroupCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RestoreGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        group.IsDeleted = false;
        _context.Set<Group>().Update(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}