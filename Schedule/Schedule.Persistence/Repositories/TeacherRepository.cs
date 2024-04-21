using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TeacherRepository : Repository, ITeacherRepository
{
    private readonly IAccountRepository _accountRepository;

    public TeacherRepository(IScheduleDbContext context,
        IAccountRepository accountRepository) : base(context)
    {
        accountRepository.UseContext(context);
        _accountRepository = accountRepository;
    }

    public async Task<int> CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        var id = default(int);

        await Context.WithTransactionAsync(async () =>
        {
            var accountId = await _accountRepository.CreateAsync(teacher.Account, cancellationToken);

            var created = await Context.Teachers.AddAsync(new Teacher
            {
                AccountId = accountId,
            }, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            id = created.Entity.TeacherId;
        }, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var teacherDb = await Context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TeacherId == teacher.TeacherId, cancellationToken);

            if (teacherDb is null)
            {
                throw new NotFoundException(nameof(Teacher), teacher.TeacherId);
            }

            teacher.Account.AccountId = teacherDb.AccountId;

            await _accountRepository.UpdateAsync(teacher.Account, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var teacher = await Context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TeacherId == id, cancellationToken);

        if (teacher is null)
        {
            throw new NotFoundException(nameof(Teacher), id);
        }

        await _accountRepository.DeleteAsync(teacher.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var teacher = await Context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TeacherId == id, cancellationToken);

        if (teacher is null)
        {
            throw new NotFoundException(nameof(Teacher), id);
        }

        await _accountRepository.RestoreAsync(teacher.AccountId, cancellationToken);
    }
}