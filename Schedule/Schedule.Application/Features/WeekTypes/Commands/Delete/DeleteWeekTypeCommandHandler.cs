using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.WeekTypes.Commands.Delete;

public sealed class DeleteWeekTypeCommandHandler : IRequestHandler<DeleteWeekTypeCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteWeekTypeCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteWeekTypeCommand request, CancellationToken cancellationToken)
    {
        var weekType = await _context.WeekTypes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.WeekTypeId == request.Id, cancellationToken);
        
        if (weekType is null)
            throw new NotFoundException(nameof(WeekType), request.Id);

        _context.WeekTypes.Remove(weekType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}