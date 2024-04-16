using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Create;

public sealed class CreateAccountCommandHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IPasswordHasherService passwordHasher) : IRequestHandler<CreateAccountCommand, int>
{
    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var searchByLogin = await context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Login == request.Login, cancellationToken);

        if (searchByLogin is not null)
            throw new AlreadyExistsException($"Выбранный логин: '{request.Login}: уже занят.");

        var searchByEmail = await context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

        if (searchByEmail is not null)
            throw new AlreadyExistsException($"Выбранный email: '{request.Email}' уже занят.");

        var nameIsExists = await context.Names
            .AsNoTracking()
            .AnyAsync(e => e.Value == request.Name, cancellationToken);

        var surnameIsExists = await context.Surnames
            .AsNoTracking()
            .AnyAsync(e => e.Value == request.Surname, cancellationToken);

        var middleNameIsExists = await context.MiddleNames
            .AsNoTracking()
            .AnyAsync(e => e.Value == request.MiddleName, cancellationToken);

        var account = mapper.Map<Account>(request);
        account.PasswordHash = passwordHasher.Hash(request.Password);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (!nameIsExists)
            {
                await context.Names.AddAsync(new Name
                {
                    Value = request.Name
                }, cancellationToken);
            }

            if (!surnameIsExists)
            {
                await context.Surnames.AddAsync(new Surname
                {
                    Value = request.Name
                }, cancellationToken);
            }

            if (!middleNameIsExists)
            {
                await context.MiddleNames.AddAsync(new MiddleName
                {
                    Value = request.Name
                }, cancellationToken);
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