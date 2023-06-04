using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Delete;

public sealed class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand>
{
    private readonly IScheduleDbContext _context;

    public DeleteSessionCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Set<Session>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SessionId == request.Id, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), request.Id);

        _context.Set<Session>().Remove(session);
        await _context.SaveChangesAsync(cancellationToken);
    }
}