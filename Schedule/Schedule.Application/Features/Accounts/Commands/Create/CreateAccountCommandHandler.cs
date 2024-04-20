using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Create;

public sealed class CreateAccountCommandHandler(
    IScheduleDbContext context,
    IAccountRepository accountRepository,
    INameRepository nameRepository,
    ISurnameRepository surnameRepository,
    IMiddleNameRepository middleNameRepository,
    IMapper mapper,
    IPasswordHasherService passwordHasher) : IRequestHandler<CreateAccountCommand, int>
{
    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var searchByLogin = await accountRepository.FindByLoginAsync(request.Login, cancellationToken);

        if (searchByLogin is not null)
            throw new AlreadyExistsException($"Выбранный логин: '{request.Login}: уже занят.");

        var searchByEmail = await accountRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (searchByEmail is not null)
            throw new AlreadyExistsException($"Выбранный email: '{request.Email}' уже занят.");

        var account = mapper.Map<Account>(request);
        account.PasswordHash = passwordHasher.Hash(request.Password);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await nameRepository.AddIfNotExistAsync(request.Name, cancellationToken);
            await surnameRepository.AddIfNotExistAsync(request.Surname, cancellationToken);

            if (request.MiddleName is not null)
            {
                await middleNameRepository.AddIfNotExistAsync(request.MiddleName, cancellationToken);
            }

            var created = await context.Accounts.AddAsync(account, cancellationToken);

            switch (request.RoleId)
            {
                case (int)AccountRole.Student:
                    await context.Students.AddAsync(new Student
                    {
                        AccountId = created.Entity.AccountId,
                        GroupId = request.GroupId!.Value
                    }, cancellationToken);
                    break;
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return created.Entity.AccountId;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}