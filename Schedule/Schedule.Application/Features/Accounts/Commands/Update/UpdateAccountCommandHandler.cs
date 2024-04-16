using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Update;

public sealed class UpdateAccountCommandHandler(IScheduleDbContext context)
    : IRequestHandler<UpdateAccountCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        if (request.Name is not null)
        {
            var nameIsExists = await context.Names
                .AsNoTracking()
                .AnyAsync(e => e.Value == request.Name, cancellationToken);

            if (!nameIsExists)
            {
                await context.Names.AddAsync(new Name
                {
                    Value = request.Name
                }, cancellationToken);
            }

            account.Name = request.Name;
        }

        if (request.Surname is not null)
        {
            var surnameIsExists = await context.Surnames
                .AsNoTracking()
                .AnyAsync(e => e.Value == request.Surname, cancellationToken);

            if (!surnameIsExists)
            {
                await context.Surnames.AddAsync(new Surname
                {
                    Value = request.Surname
                }, cancellationToken);
            }

            account.Surname = request.Surname;
        }

        if (request.MiddleName is not null)
        {
            var middleNameIsExists = await context.MiddleNames
                .AsNoTracking()
                .AnyAsync(e => e.Value == request.MiddleName, cancellationToken);

            if (!middleNameIsExists)
            {
                await context.MiddleNames.AddAsync(new MiddleName
                {
                    Value = request.MiddleName
                }, cancellationToken);
            }

            account.MiddleName = request.MiddleName;
        }

        if (request.Email is not null)
        {
            var searchByEmail = await context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

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