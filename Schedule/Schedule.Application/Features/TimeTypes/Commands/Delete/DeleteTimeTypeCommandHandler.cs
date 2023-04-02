using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.TimeTypes.Commands.Delete;

public sealed class DeleteTimeTypeCommandHandler : IRequestHandler<DeleteTimeTypeCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteTimeTypeCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeType = await _context.TimeTypes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);
        
        if (timeType is null)
            throw new NotFoundException(nameof(TimeType), request.Id);

        _context.TimeTypes.Remove(timeType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}