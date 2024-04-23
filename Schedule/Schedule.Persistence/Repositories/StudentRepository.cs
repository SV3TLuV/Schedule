using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class StudentRepository : Repository, IStudentRepository
{
    private readonly IAccountRepository _accountRepository;

    public StudentRepository(IScheduleDbContext context,
        IAccountRepository accountRepository) : base(context)
    {
        accountRepository.UseContext(context);
        _accountRepository = accountRepository;
    }

    public async Task<int> CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var id = default(int);

        await Context.WithTransactionAsync(async () =>
        {
            student.Account.RoleId = (int)AccountRole.Student;

            var accountId = await _accountRepository.CreateAsync(student.Account, cancellationToken);

            var created = await Context.Students.AddAsync(new Student
            {
                AccountId = accountId,
                GroupId = student.GroupId
            }, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            id = created.Entity.StudentId;
        }, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var studentDb = await Context.Students.FirstOrDefaultAsync(e =>
                e.StudentId == student.StudentId, cancellationToken);

            if (studentDb is null)
            {
                throw new NotFoundException(nameof(Student), student.StudentId);
            }

            student.Account.AccountId = studentDb.AccountId;

            await _accountRepository.UpdateAsync(student.Account, cancellationToken);

            studentDb.GroupId = student.GroupId;

            Context.Students.Update(studentDb);
            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var student = await Context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.StudentId == id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException(nameof(Student), id);
        }

        await _accountRepository.DeleteAsync(student.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var student = await Context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.StudentId == id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException(nameof(Student), id);
        }

        await _accountRepository.RestoreAsync(student.AccountId, cancellationToken);
    }
}