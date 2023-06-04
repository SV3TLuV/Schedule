using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Update;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Refresh;

public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthorizationResultViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public RefreshCommandHandler(
        IScheduleDbContext context,
        ITokenService tokenService,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<AuthorizationResultViewModel> Handle(RefreshCommand request,
        CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var sidClaim = principal.FindFirst(nameof(ClaimTypes.Sid));

        if (sidClaim is null || !Guid.TryParse(sidClaim.Value, out var sessionId))
            throw new NotFoundException("Invalid AccessToken");

        var session = await _context.Set<Session>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.User)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(e => e.SessionId == sessionId, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), sessionId);

        if (session.RefreshToken != request.RefreshToken)
            throw new NotFoundException("Invalid RefreshToken");

        var command = new UpdateSessionCommand
        {
            Id = session.SessionId,
            RefreshToken = _tokenService.GenerateRefreshToken(),
            UserId = session.UserId
        };

        await _mediator.Send(command, cancellationToken);
        var userViewModel = _mapper.Map<UserViewModel>(session.User);
        var accessToken = _tokenService.GenerateAccessToken(session.User, session.SessionId);

        return new AuthorizationResultViewModel
        {
            AccessToken = accessToken,
            RefreshToken = command.RefreshToken,
            User = userViewModel
        };
    }
}