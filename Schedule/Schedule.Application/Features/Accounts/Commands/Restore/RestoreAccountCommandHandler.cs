using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Restore;

public sealed class RestoreAccountCommandHandler(
    IScheduleDbContext context) : IRequestHandler<RestoreAccountCommand, Unit>
{
    public async Task<Unit> Handle(RestoreAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        account.IsDeleted = false;

        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}