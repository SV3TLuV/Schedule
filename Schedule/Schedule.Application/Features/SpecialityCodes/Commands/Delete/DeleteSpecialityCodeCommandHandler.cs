using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Delete;

public sealed class DeleteSpecialityCodeCommandHandler : IRequestHandler<DeleteSpecialityCodeCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteSpecialityCodeCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteSpecialityCodeCommand request, CancellationToken cancellationToken)
    {
        var specialityCode = await _context.SpecialityCodes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityCodeId == request.Id, cancellationToken);
        
        if (specialityCode is null)
            throw new NotFoundException(nameof(SpecialityCode), request.Id);

        _context.SpecialityCodes.Remove(specialityCode);
        await _context.SaveChangesAsync(cancellationToken);
    }
}