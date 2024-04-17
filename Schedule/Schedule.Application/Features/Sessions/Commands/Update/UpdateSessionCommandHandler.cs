using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Update;

public sealed class UpdateSessionCommandHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IDateInfoService dateInfoService) : IRequestHandler<UpdateSessionCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var sessionDbo = await context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SessionId == request.Id, cancellationToken);

        if (sessionDbo is null)
            throw new NotFoundException(nameof(Session), request.Id);

        var session = mapper.Map<Session>(request);
        session.Updated = dateInfoService.CurrentDate;

        context.Sessions.Update(session);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}