using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Delete;

public sealed class DeleteSpecialityCodeCommandHandler : IRequestHandler<DeleteSpecialityCodeCommand>
{
    private readonly IScheduleDbContext _context;

    public DeleteSpecialityCodeCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSpecialityCodeCommand request, CancellationToken cancellationToken)
    {
        var specialityCode = await _context.Set<SpecialityCode>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityCodeId == request.Id, cancellationToken);

        if (specialityCode is null)
            throw new NotFoundException(nameof(SpecialityCode), request.Id);

        _context.Set<SpecialityCode>().Remove(specialityCode);
        await _context.SaveChangesAsync(cancellationToken);
    }
}