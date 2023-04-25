using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Persistence.Context;

public partial class ScheduleDbContext : DbContext, IScheduleDbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classroom> Classrooms { get; set; } = null!;

    public virtual DbSet<ClassroomType> ClassroomTypes { get; set; } = null!;

    public virtual DbSet<Course> Courses { get; set; } = null!;

    public virtual DbSet<Date> Dates { get; set; } = null!;

    public virtual DbSet<Day> Days { get; set; } = null!;

    public virtual DbSet<Discipline> Disciplines { get; set; } = null!;

    public virtual DbSet<Group> Groups { get; set; } = null!;

    public virtual DbSet<Lesson> Lessons { get; set; } = null!;

    public virtual DbSet<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } = null!;

    public virtual DbSet<LessonTemplate> LessonTemplates { get; set; } = null!;

    public virtual DbSet<LessonTemplateTeacherClassroom> LessonTemplateTeacherClassrooms { get; set; } = null!;

    public virtual DbSet<Speciality> Specialities { get; set; } = null!;

    public virtual DbSet<Teacher> Teachers { get; set; } = null!;

    public virtual DbSet<Template> Templates { get; set; } = null!;

    public virtual DbSet<Term> Terms { get; set; } = null!;

    public virtual DbSet<Time> Times { get; set; } = null!;

    public virtual DbSet<TimeType> TimeTypes { get; set; } = null!;

    public virtual DbSet<Timetable> Timetables { get; set; } = null!;

    public virtual DbSet<WeekType> WeekTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleDbContext).Assembly);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
