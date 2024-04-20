using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Sessions.Commands.Create;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Login;

public sealed class LoginCommandHandler(
    IScheduleDbContext context,
    IAccountRepository accountRepository,
    ITokenService tokenService,
    IMediator mediator,
    IMapper mapper,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<LoginCommand, AuthorizationResultViewModel>
{
    public async Task<AuthorizationResultViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.FindByLoginAsync(request.Login, cancellationToken);

        if (account is null || !passwordHasher.EnhancedHash(request.Password, account.PasswordHash))
            throw new IncorrectAuthorizationDataException();

        var command = new CreateSessionCommand
        {
            Id = Guid.NewGuid(),
            RefreshToken = tokenService.GenerateRefreshToken(),
            AccountId = account.AccountId
        };

        await mediator.Send(command, cancellationToken);
        var accountViewModel = mapper.Map<AccountViewModel>(account);
        var accessToken = tokenService.GenerateAccessToken(account, command.Id);

        return new AuthorizationResultViewModel
        {
            AccessToken = accessToken,
            RefreshToken = command.RefreshToken,
            Account = accountViewModel
        };
    }
}