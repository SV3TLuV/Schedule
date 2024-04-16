using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Delete;

public sealed class DeleteAccountCommandHandler(
    IScheduleDbContext context,
    IMediator mediator) : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(Account), request.Id);

        context.Accounts.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}