using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler(
    IScheduleDbContext context,
    IPasswordHasherService passwordHasher) : IRequestHandler<ChangePasswordCommand, Unit>
{
    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        account.PasswordHash = passwordHasher.Hash(request.Password);

        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}