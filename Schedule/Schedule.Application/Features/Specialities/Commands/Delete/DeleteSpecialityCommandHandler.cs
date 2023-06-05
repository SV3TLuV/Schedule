using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

public sealed class DeleteSpecialityCommandHandler : IRequestHandler<DeleteSpecialityCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteSpecialityCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _context.Set<Speciality>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (speciality is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        _context.Set<Speciality>().Remove(speciality);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}