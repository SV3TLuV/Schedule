using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Groups.Commands.Delete;

public sealed class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteGroupCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);
        
        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}