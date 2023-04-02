using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Times.Commands.Delete;

public sealed class DeleteTimeCommandHandler : IRequestHandler<DeleteTimeCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteTimeCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteTimeCommand request, CancellationToken cancellationToken)
    {
        var time = await _context.Times
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);
        
        if (time is null)
            throw new NotFoundException(nameof(Time), request.Id);

        _context.Times.Remove(time);
        await _context.SaveChangesAsync(cancellationToken);
    }
}