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

    public virtual DbSet<Classroom> Classrooms { get; set; } = null!;

    public virtual DbSet<ClassroomType> ClassroomTypes { get; set; } = null!;

    public virtual DbSet<Course> Courses { get; set; } = null!;

    public virtual DbSet<Date> Dates { get; set; } = null!;

    public virtual DbSet<Day> Days { get; set; } = null!;

    public virtual DbSet<Discipline> Disciplines { get; set; } = null!;

    public virtual DbSet<Group> Groups { get; set; } = null!;

    public virtual DbSet<Lesson> Lessons { get; set; } = null!;

    public virtual DbSet<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } = null!;

    public virtual DbSet<SpecialityCode> SpecialityCodes { get; set; } = null!;

    public virtual DbSet<Teacher> Teachers { get; set; } = null!;

    public virtual DbSet<Template> Templates { get; set; } = null!;

    public virtual DbSet<Term> Terms { get; set; } = null!;

    public virtual DbSet<Time> Times { get; set; } = null!;

    public virtual DbSet<TimeType> TimeTypes { get; set; } = null!;

    public virtual DbSet<Timetable> Timetables { get; set; } = null!;

    public virtual DbSet<WeekType> WeekTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Classrooms_Delete"));

            entity.HasIndex(e => e.Cabinet, "IX_Classrooms").IsUnique();

            entity.Property(e => e.Cabinet).HasMaxLength(10);

            entity.HasMany(d => d.ClassroomTypes).WithMany(p => p.Classrooms)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassroomClassroomType",
                    r => r.HasOne<ClassroomType>().WithMany()
                        .HasForeignKey("ClassroomTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClassroomClassroomTypes_ClassroomTypes"),
                    l => l.HasOne<Classroom>().WithMany()
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClassroomClassroomTypes_Classrooms"),
                    j =>
                    {
                        j.HasKey("ClassroomId", "ClassroomTypeId");
                        j.ToTable("ClassroomClassroomTypes");
                    });
        });

        modelBuilder.Entity<ClassroomType>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ClassroomTypes").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity => { entity.Property(e => e.CourseId).ValueGeneratedNever(); });

        modelBuilder.Entity<Date>(entity =>
        {
            entity.HasIndex(e => e.Value, "IX_Dates").IsUnique();

            entity.Property(e => e.Value).HasColumnType("date");

            entity.HasOne(d => d.Day).WithMany(p => p.Dates)
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dates_Days");

            entity.HasOne(d => d.TimeType).WithMany(p => p.Dates)
                .HasForeignKey(d => d.TimeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dates_TimeTypes");

            entity.HasOne(d => d.WeekType).WithMany(p => p.Dates)
                .HasForeignKey(d => d.WeekTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dates_WeekTypes");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Days").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Disciplines_Delete"));

            entity.HasIndex(e => new { e.Code, e.Name }, "IX_Disciplines").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.SpecialityCode).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.SpecialityCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disciplines_SpecialityCodes");

            entity.HasOne(d => d.Term).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.TermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disciplines_Terms");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Groups_Delete"));

            entity.HasIndex(e => new { e.Number, e.EnrollmentYear }, "IX_Groups").IsUnique();

            entity.Property(e => e.Number)
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Courses");

            entity.HasOne(d => d.SpecialityCode).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SpecialityCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_SpecialityCodes");

            entity.HasMany(d => d.Groups2).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupGroup",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId2")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GroupGroups_Groups1"),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GroupGroups_Groups"),
                    j =>
                    {
                        j.HasKey("GroupId", "GroupId2");
                        j.ToTable("GroupGroups");
                    });

            entity.HasMany(d => d.Groups).WithMany(p => p.Groups2)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupGroup",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GroupGroups_Groups"),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId2")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GroupGroups_Groups1"),
                    j =>
                    {
                        j.HasKey("GroupId", "GroupId2");
                        j.ToTable("GroupGroups");
                    });
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK_Pairs");

            entity.HasOne(d => d.Discipline).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("FK_Pairs_Disciplines");

            entity.HasOne(d => d.Time).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimeId)
                .HasConstraintName("FK_Pairs_Times");

            entity.HasOne(d => d.Template).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimetableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_Templates");

            entity.HasOne(d => d.Timetable).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimetableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_Timetables");
        });

        modelBuilder.Entity<LessonTeacherClassroom>(entity =>
        {
            entity.HasKey(e => new { e.LessonId, e.TeacherId }).HasName("PK_PairTeachers");

            entity.HasOne(d => d.Classroom).WithMany(p => p.LessonTeacherClassrooms)
                .HasForeignKey(d => d.ClassroomId)
                .HasConstraintName("FK_LessonTeacherClassrooms_Classrooms");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonTeacherClassrooms)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PairTeachers_Pairs");

            entity.HasOne(d => d.Teacher).WithMany(p => p.LessonTeacherClassrooms)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PairTeachers_Teachers");
        });

        modelBuilder.Entity<SpecialityCode>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("SpecialityCodes_Delete"));

            entity.HasIndex(e => new { e.Code, e.Name }, "IX_SpecialityCodes").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Teachers_Delete"));

            entity.HasIndex(e => e.Email, "IX_Teachers").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.MiddleName).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(40);
            entity.Property(e => e.Surname).HasMaxLength(40);

            entity.HasMany(d => d.Disciplines).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherDiscipline",
                    r => r.HasOne<Discipline>().WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherDisciplines_Disciplines"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherDisciplines_Teachers"),
                    j =>
                    {
                        j.HasKey("TeacherId", "DisciplineId");
                        j.ToTable("TeacherDisciplines");
                    });

            entity.HasMany(d => d.Groups).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherGroup",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherGroups_Groups"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeacherGroups_Teachers"),
                    j =>
                    {
                        j.HasKey("TeacherId", "GroupId");
                        j.ToTable("TeacherGroups");
                    });
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasIndex(e => new { e.GroupId, e.TermId, e.DayId, e.WeekTypeId }, "IX_Templates_1").IsUnique();

            entity.HasOne(d => d.Day).WithMany(p => p.Templates)
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Templates_Days");

            entity.HasOne(d => d.Group).WithMany(p => p.Templates)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Templates_Groups");

            entity.HasOne(d => d.Term).WithMany(p => p.Templates)
                .HasForeignKey(d => d.TermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Templates_Terms");

            entity.HasOne(d => d.WeekType).WithMany(p => p.Templates)
                .HasForeignKey(d => d.WeekTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Templates_WeekTypes");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasIndex(e => new { e.CourseId, e.CourseTerm }, "IX_Terms").IsUnique();

            entity.Property(e => e.TermId).ValueGeneratedNever();

            entity.HasOne(d => d.Course).WithMany(p => p.Terms)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Terms_Courses");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Times_Delete"));

            entity.HasIndex(e => new { e.TypeId, e.LessonNumber }, "IX_Times").IsUnique();

            entity.HasOne(d => d.Type).WithMany(p => p.Times)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Times_TimeTypes");
        });

        modelBuilder.Entity<TimeType>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TimeTypes_Delete"));

            entity.HasIndex(e => e.Name, "IX_TimeTypes").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasIndex(e => new { e.DateId, e.GroupId }, "IX_Timetables").IsUnique();

            entity.HasOne(d => d.Date).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.DateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetables_Dates");

            entity.HasOne(d => d.Group).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetables_Groups");
        });

        modelBuilder.Entity<WeekType>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_WeekTypes").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}