using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Delete;

public sealed class DeleteTimeTypeCommandHandler : IRequestHandler<DeleteTimeTypeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteTimeTypeCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeType = await _context.Set<TimeType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);

        if (timeType is null)
            throw new NotFoundException(nameof(TimeType), request.Id);

        _context.Set<TimeType>().Remove(timeType);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}