using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Delete;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public LogoutCommandHandler(
        IScheduleDbContext context,
        ITokenService tokenService,
        IMediator mediator)
    {
        _context = context;
        _tokenService = tokenService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var sidClaim = principal.FindFirst(ClaimTypes.Sid);

        if (sidClaim is null || !Guid.TryParse(sidClaim.Value, out var sessionId))
            throw new NotFoundException("Invalid AccessToken");

        var session = await _context.Set<Session>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.SessionId == sessionId, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), sessionId);

        if (session.RefreshToken != request.RefreshToken)
            throw new NotFoundException("Invalid RefreshToken");

        await _mediator.Send(new DeleteSessionCommand(sessionId), cancellationToken);
        return Unit.Value;
    }
}