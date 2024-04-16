using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Delete;

public sealed class DeleteAccountCommandHandler(IScheduleDbContext context) : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        account.IsDeleted = true;

        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}