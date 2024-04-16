using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Persistence.Context;

public partial class ScheduleDbContext : DbContext, IScheduleDbContext
{
    public ScheduleDbContext()
    {
    }

    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<DisciplineCode> DisciplineCodes { get; set; }

    public virtual DbSet<DisciplineName> DisciplineNames { get; set; }

    public virtual DbSet<DisciplineType> DisciplineTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePermission> EmployeePermissions { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupTransfer> GroupTransfers { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonChange> LessonChanges { get; set; }

    public virtual DbSet<MiddleName> MiddleNames { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Surname> Surnames { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<WeekType> WeekTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("en_US.UTF-8");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
