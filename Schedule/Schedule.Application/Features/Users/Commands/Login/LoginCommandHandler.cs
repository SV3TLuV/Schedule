using AutoMapper;
using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Create;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthorizationResultViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(
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

    public async Task<AuthorizationResultViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Role)
            .FirstOrDefaultAsync(e => e.Login == request.Login, cancellationToken);

        if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash, HashType.SHA512))
            throw new AuthorizationException();

        var command = new CreateSessionCommand
        {
            Id = Guid.NewGuid(),
            RefreshToken = _tokenService.GenerateRefreshToken(),
            UserId = user.UserId
        };
        
        await _mediator.Send(command, cancellationToken);
        var userViewModel = _mapper.Map<UserViewModel>(user);
        var accessToken = _tokenService.GenerateAccessToken(user, command.Id);

        return new AuthorizationResultViewModel
        {
            AccessToken = accessToken,
            RefreshToken = command.RefreshToken,
            User = userViewModel
        };
    }
}