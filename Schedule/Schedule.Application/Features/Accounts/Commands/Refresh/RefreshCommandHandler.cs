﻿using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Update;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Refresh;

public sealed class RefreshCommandHandler(
    IScheduleDbContext context,
    ITokenService tokenService,
    IMediator mediator,
    IMapper mapper)
    : IRequestHandler<RefreshCommand, AuthorizationResultViewModel>
{
    public async Task<AuthorizationResultViewModel> Handle(RefreshCommand request,
        CancellationToken cancellationToken)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var sidClaim = principal.FindFirst(ClaimTypes.Sid);

        if (sidClaim is null || !Guid.TryParse(sidClaim.Value, out var sessionId))
            throw new NotFoundException("Invalid AccessToken");

        var session = await context.Sessions
            .AsNoTracking()
            .Include(e => e.Account)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(e => e.SessionId == sessionId, cancellationToken);

        if (session is null)
            throw new NotFoundException(nameof(Session), sessionId);

        if (session.RefreshToken != request.RefreshToken)
            throw new NotFoundException("Invalid RefreshToken");

        var command = new UpdateSessionCommand
        {
            Id = session.SessionId,
            RefreshToken = tokenService.GenerateRefreshToken(),
            UserId = session.AccountId
        };

        await mediator.Send(command, cancellationToken);
        var accountViewModel = mapper.Map<AccountViewModel>(session.Account);
        var accessToken = tokenService.GenerateAccessToken(session.Account, session.SessionId);

        return new AuthorizationResultViewModel
        {
            AccessToken = accessToken,
            RefreshToken = command.RefreshToken,
            Account = accountViewModel
        };
    }
}