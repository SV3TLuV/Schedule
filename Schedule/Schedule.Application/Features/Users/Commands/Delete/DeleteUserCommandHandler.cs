using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteUserCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(User), request.Id);

        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}