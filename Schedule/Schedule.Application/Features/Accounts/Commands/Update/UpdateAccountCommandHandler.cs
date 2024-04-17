using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Update;

public sealed class UpdateAccountCommandHandler(
    IScheduleDbContext context,
    IAccountRepository accountRepository,
    INameRepository nameRepository,
    ISurnameRepository surnameRepository,
    IMiddleNameRepository middleNameRepository) : IRequestHandler<UpdateAccountCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        if (account.IsDeleted)
            throw new DeletedException(nameof(Account));

        if (request.Name is not null)
        {
            await nameRepository.AddIfNotExist(request.Name, cancellationToken);
            account.Name = request.Name;
        }

        if (request.Surname is not null)
        {
            await surnameRepository.AddIfNotExist(request.Surname, cancellationToken);
            account.Surname = request.Surname;
        }

        if (request.MiddleName is not null)
        {
            await middleNameRepository.AddIfNotExist(request.MiddleName, cancellationToken);
            account.MiddleName = request.MiddleName;
        }

        if (request.Email is not null)
        {
            var searchByEmail = await accountRepository.FindByEmail(request.Email, cancellationToken);

            if (searchByEmail is not null)
                throw new AlreadyExistsException($"Выбранный email: '{request.Email}' уже занят.");

            account.Email = request.Email;
        }

        switch (account.RoleId)
        {
            case (int)AccountRole.Student:
                if (request.GroupId is not null)
                {
                    var student = await context.Students.FirstAsync(e =>
                        e.AccountId == request.Id, cancellationToken);

                    student.GroupId = request.GroupId.Value;
                }
                break;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}