using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Delete;

public sealed class DeleteTimeCommandHandler : IRequestHandler<DeleteTimeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteTimeCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTimeCommand request, CancellationToken cancellationToken)
    {
        var time = await _context.Set<Time>()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (time is null)
            throw new NotFoundException(nameof(Time), request.Id);

        time.IsDeleted = true;
        
        _context.Set<Time>().Update(time);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}