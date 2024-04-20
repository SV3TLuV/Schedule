using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Persistence.Context;

public class ScheduleDbContext : DbContext, IScheduleDbContext
{
    public ScheduleDbContext()
    {
    }

    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; init; }

    public virtual DbSet<Classroom> Classrooms { get; init; }

    public virtual DbSet<Day> Days { get; init; }

    public virtual DbSet<Discipline> Disciplines { get; init; }

    public virtual DbSet<DisciplineCode> DisciplineCodes { get; init; }

    public virtual DbSet<DisciplineName> DisciplineNames { get; init; }

    public virtual DbSet<DisciplineType> DisciplineTypes { get; init; }

    public virtual DbSet<Employee> Employees { get; init; }

    public virtual DbSet<EmployeePermission> EmployeePermissions { get; init; }

    public virtual DbSet<Group> Groups { get; init; }

    public virtual DbSet<GroupTransfer> GroupTransfers { get; init; }

    public virtual DbSet<Lesson> Lessons { get; init; }

    public virtual DbSet<LessonChange> LessonChanges { get; init; }

    public virtual DbSet<MiddleName> MiddleNames { get; init; }

    public virtual DbSet<Name> Names { get; init; }

    public virtual DbSet<Permission> Permissions { get; init; }

    public virtual DbSet<Role> Roles { get; init; }

    public virtual DbSet<Session> Sessions { get; init; }

    public virtual DbSet<Speciality> Specialities { get; init; }

    public virtual DbSet<Student> Students { get; init; }

    public virtual DbSet<Surname> Surnames { get; init; }

    public virtual DbSet<Teacher> Teachers { get; init; }

    public virtual DbSet<Term> Terms { get; init; }

    public virtual DbSet<Timetable> Timetables { get; init; }

    public virtual DbSet<WeekType> WeekTypes { get; init; }

    public async Task WithTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction != null)
        {
            await action();
        }
        else
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("en_US.UTF-8");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleDbContext).Assembly);
    }
}
