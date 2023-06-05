using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Restore;

public sealed class RestoreTimeTypeCommandHandler : IRequestHandler<RestoreTimeTypeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreTimeTypeCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RestoreTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeType = await _context.Set<TimeType>()
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);

        if (timeType is null)
            throw new NotFoundException(nameof(TimeType), request.Id);

        timeType.IsDeleted = false;
        _context.Set<TimeType>().Update(timeType);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}