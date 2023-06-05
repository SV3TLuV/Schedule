using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Restore;

public sealed class RestoreTimeCommandHandler : IRequestHandler<RestoreTimeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreTimeCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RestoreTimeCommand request, CancellationToken cancellationToken)
    {
        var time = await _context.Set<Time>()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (time is null)
            throw new NotFoundException(nameof(Time), request.Id);

        time.IsDeleted = false;
        _context.Set<Time>().Update(time);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}