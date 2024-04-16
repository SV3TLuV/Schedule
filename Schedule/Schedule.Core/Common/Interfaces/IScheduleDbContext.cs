using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schedule.Core.Models;

namespace Schedule.Core.Common.Interfaces;

public interface IScheduleDbContext
{
    DatabaseFacade Database { get; }

    DbSet<Account> Accounts { get; }

    DbSet<Classroom> Classrooms { get; }

    DbSet<Day> Days { get; }

    DbSet<Discipline> Disciplines { get; }

    DbSet<DisciplineCode> DisciplineCodes { get; }

    DbSet<DisciplineName> DisciplineNames { get; }

    DbSet<DisciplineType> DisciplineTypes { get; }

    DbSet<Employee> Employees { get; }

    DbSet<EmployeePermission> EmployeePermissions { get; }

    DbSet<Group> Groups { get; }

    DbSet<GroupTransfer> GroupTransfers { get; }

    DbSet<Lesson> Lessons { get; }

    DbSet<LessonChange> LessonChanges { get; }

    DbSet<MiddleName> MiddleNames { get; }

    DbSet<Name> Names { get; }

    DbSet<Permission> Permissions { get; }

    DbSet<Role> Roles { get; }

    DbSet<Session> Sessions { get; }

    DbSet<Speciality> Specialities { get; }

    DbSet<Student> Students { get; }

    DbSet<Surname> Surnames { get; }

    DbSet<Teacher> Teachers { get; }

    DbSet<Term> Terms { get; }

    DbSet<Timetable> Timetables { get; }

    DbSet<WeekType> WeekTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}