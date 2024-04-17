using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Delete;

public sealed class DeleteSessionCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteSessionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SessionId == request.Id, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), request.Id);
        
        context.Sessions.Remove(session);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}