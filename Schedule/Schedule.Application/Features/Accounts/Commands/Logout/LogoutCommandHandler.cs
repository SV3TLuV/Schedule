using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Delete;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Logout;

public sealed class LogoutCommandHandler(
    IScheduleDbContext context,
    ITokenService tokenService,
    IMediator mediator)
    : IRequestHandler<LogoutCommand, Unit>
{
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var sidClaim = principal.FindFirst(ClaimTypes.Sid);

        if (sidClaim is null || !Guid.TryParse(sidClaim.Value, out var sessionId))
            throw new NotFoundException("Invalid AccessToken");

        var session = await context.Sessions
            .AsNoTracking()
            .Include(e => e.Account)
            .FirstOrDefaultAsync(e => e.SessionId == sessionId, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), sessionId);

        if (session.RefreshToken != request.RefreshToken)
            throw new NotFoundException("Invalid RefreshToken");

        if (request.IsAllDevices)
        {
            await context.Sessions
                .AsNoTracking()
                .Where(e => e.AccountId == session.AccountId)
                .ExecuteDeleteAsync(cancellationToken);
        }
        else
        {
            await mediator.Send(new DeleteSessionCommand(sessionId), cancellationToken);
        }

        return Unit.Value;
    }
}