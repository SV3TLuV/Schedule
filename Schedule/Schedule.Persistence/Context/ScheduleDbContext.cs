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

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<TimeType> TimeTypes { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<WeekType> WeekTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=P@ssw0rd;Host=host.docker.internal;Port=5432;Database=schedule;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("en_US.UTF-8");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pk");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "account_email_index").IsUnique();

            entity.HasIndex(e => e.Login, "account_login_index").IsUnique();

            entity.Property(e => e.AccountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("account_id");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(40)
                .HasColumnName("middle_name");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(512)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(40)
                .HasColumnName("surname");

            entity.HasOne(d => d.MiddleNameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.MiddleName)
                .HasConstraintName("account_middle_name_fk");

            entity.HasOne(d => d.NameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Name)
                .HasConstraintName("account_name_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("account_role_id_fk");

            entity.HasOne(d => d.SurnameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Surname)
                .HasConstraintName("account_surname_fk");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("classroom_pk");

            entity.ToTable("classroom");

            entity.HasIndex(e => e.Cabinet, "classroom_cabinet_index").IsUnique();

            entity.Property(e => e.ClassroomId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("classroom_id");
            entity.Property(e => e.Cabinet)
                .HasMaxLength(10)
                .HasColumnName("cabinet");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("day_pk");

            entity.ToTable("day");

            entity.HasIndex(e => e.Name, "day_name_index").IsUnique();

            entity.Property(e => e.DayId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("day_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.DisciplineId).HasName("discipline_pk");

            entity.ToTable("discipline");

            entity.HasIndex(e => new { e.DisciplineCodeId, e.DisciplineNameId, e.SpecialityId, e.TermId }, "discipline_index").IsUnique();

            entity.Property(e => e.DisciplineId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discipline_id");
            entity.Property(e => e.DisciplineCodeId).HasColumnName("discipline_code_id");
            entity.Property(e => e.DisciplineNameId).HasColumnName("discipline_name_id");
            entity.Property(e => e.DisciplineTypeId).HasColumnName("discipline_type_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");
            entity.Property(e => e.TermId).HasColumnName("term_id");
            entity.Property(e => e.TotalHours).HasColumnName("total_hours");

            entity.HasOne(d => d.DisciplineCode).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.DisciplineCodeId)
                .HasConstraintName("discipline_code_id_fk");

            entity.HasOne(d => d.DisciplineName).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.DisciplineNameId)
                .HasConstraintName("discipline_name_id_fk");

            entity.HasOne(d => d.DisciplineType).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.DisciplineTypeId)
                .HasConstraintName("discipline_type_id_fk");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.SpecialityId)
                .HasConstraintName("discipline_speciality_id_fk");

            entity.HasOne(d => d.Term).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.TermId)
                .HasConstraintName("discipline_term_id_fk");
        });

        modelBuilder.Entity<DisciplineCode>(entity =>
        {
            entity.HasKey(e => e.DisciplineCodeId).HasName("discipline_code_pk");

            entity.ToTable("discipline_code");

            entity.HasIndex(e => e.Code, "discipline_code_code_index").IsUnique();

            entity.Property(e => e.DisciplineCodeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discipline_code_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
        });

        modelBuilder.Entity<DisciplineName>(entity =>
        {
            entity.HasKey(e => e.DisciplineNameId).HasName("discipline_name_pk");

            entity.ToTable("discipline_name");

            entity.HasIndex(e => e.Name, "discipline_name_name_index").IsUnique();

            entity.Property(e => e.DisciplineNameId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discipline_name_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DisciplineType>(entity =>
        {
            entity.HasKey(e => e.DisciplineTypeId).HasName("discipline_type_pk");

            entity.ToTable("discipline_type");

            entity.Property(e => e.DisciplineTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discipline_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pk");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("employee_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_account_id_fk");
        });

        modelBuilder.Entity<EmployeePermission>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.PermissionId }).HasName("employee_permission_pk");

            entity.ToTable("employee_permission");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.HasAccess)
                .HasDefaultValue(false)
                .HasColumnName("has_access");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePermissions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employee_permission_employee_id_fk");

            entity.HasOne(d => d.Permission).WithMany(p => p.EmployeePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("employee_permission_permission_id_fk");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("group_pk");

            entity.ToTable("group");

            entity.HasIndex(e => new { e.Number, e.EnrollmentYear, e.SpecialityId }, "group_index").IsUnique();

            entity.Property(e => e.GroupId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("group_id");
            entity.Property(e => e.EnrollmentYear).HasColumnName("enrollment_year");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Number)
                .HasMaxLength(2)
                .HasColumnName("number");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");
            entity.Property(e => e.TermId).HasColumnName("term_id");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SpecialityId)
                .HasConstraintName("group_speciality_id_fk");

            entity.HasOne(d => d.Term).WithMany(p => p.Groups)
                .HasForeignKey(d => d.TermId)
                .HasConstraintName("group_term_id_fk");
        });

        modelBuilder.Entity<GroupTransfer>(entity =>
        {
            entity.HasKey(e => new { e.NextTermId, e.GroupId }).HasName("group_transfer_pk");

            entity.ToTable("group_transfer");

            entity.Property(e => e.NextTermId).HasColumnName("next_term_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.IsTransferred).HasColumnName("is_transferred");
            entity.Property(e => e.TransferDate).HasColumnName("transfer_date");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupTransfers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_transfer_group_id_fk");

            entity.HasOne(d => d.NextTerm).WithMany(p => p.GroupTransfers)
                .HasForeignKey(d => d.NextTermId)
                .HasConstraintName("group_transfer_next_term_id_fk");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("lesson_pk");

            entity.ToTable("lesson");

            entity.Property(e => e.LessonId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lesson_id");
            entity.Property(e => e.ClassroomIds).HasColumnName("classroom_ids");
            entity.Property(e => e.DisciplineId).HasColumnName("discipline_id");
            entity.Property(e => e.LessonChangeId).HasColumnName("lesson_change_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Subgroup).HasColumnName("subgroup");
            entity.Property(e => e.TeacherIds).HasColumnName("teacher_ids");
            entity.Property(e => e.TimeId).HasColumnName("time_id");
            entity.Property(e => e.TimetableId).HasColumnName("timetable_id");

            entity.HasOne(d => d.Discipline).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("lesson_discipline_id_fk");

            entity.HasOne(d => d.LessonChange).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LessonChangeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_lesson_change_id_fk");

            entity.HasOne(d => d.Time).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_time_id_fk");

            entity.HasOne(d => d.Timetable).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimetableId)
                .HasConstraintName("lesson_timetable_id_fk");
        });

        modelBuilder.Entity<LessonChange>(entity =>
        {
            entity.HasKey(e => e.LessonChangeId).HasName("lesson_change_pk");

            entity.ToTable("lesson_change");

            entity.Property(e => e.LessonChangeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lesson_change_id");
            entity.Property(e => e.ClassroomIds).HasColumnName("classroom_ids");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DisciplineId).HasColumnName("discipline_id");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Subgroup).HasColumnName("subgroup");
            entity.Property(e => e.TeacherIds).HasColumnName("teacher_ids");
            entity.Property(e => e.TimeId).HasColumnName("time_id");

            entity.HasOne(d => d.Discipline).WithMany(p => p.LessonChanges)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("lesson_change_discipline_id_fk");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonChanges)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("lesson_change_lesson_id_fk");

            entity.HasOne(d => d.Time).WithMany(p => p.LessonChanges)
                .HasForeignKey(d => d.TimeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_change_time_id_fk");
        });

        modelBuilder.Entity<MiddleName>(entity =>
        {
            entity.HasKey(e => e.MiddleName1).HasName("middle_name_pk");

            entity.ToTable("middle_name");

            entity.Property(e => e.MiddleName1)
                .HasMaxLength(40)
                .HasColumnName("middle_name");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.HasKey(e => e.Name1).HasName("name_pk");

            entity.ToTable("name");

            entity.Property(e => e.Name1)
                .HasMaxLength(40)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permission_pk");

            entity.ToTable("permission");

            entity.HasIndex(e => e.Name, "permission_name_index").IsUnique();

            entity.Property(e => e.PermissionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("permission_id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pk");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_index").IsUnique();

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("session_pk");

            entity.ToTable("session");

            entity.Property(e => e.SessionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("session_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(512)
                .HasColumnName("refresh_token");
            entity.Property(e => e.Updated).HasColumnName("updated");

            entity.HasOne(d => d.Account).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("session_account_id_fk");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.SpecialityId).HasName("speciality_pk");

            entity.ToTable("speciality");

            entity.HasIndex(e => e.Name, "speciality_name_index").IsUnique();

            entity.Property(e => e.SpecialityId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("speciality_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MaxTermId).HasColumnName("max_term_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");

            entity.HasOne(d => d.MaxTerm).WithMany(p => p.Specialities)
                .HasForeignKey(d => d.MaxTermId)
                .HasConstraintName("speciality_max_term_id_fk");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pk");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("student_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Students)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("student_account_id_fk");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("student_group_id_fk");
        });

        modelBuilder.Entity<Surname>(entity =>
        {
            entity.HasKey(e => e.Surname1).HasName("surname_pk");

            entity.ToTable("surname");

            entity.Property(e => e.Surname1)
                .HasMaxLength(40)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teacher_pk");

            entity.ToTable("teacher");

            entity.Property(e => e.TeacherId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teacher_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("teacher_account_id_fk");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.TermId).HasName("term_pk");

            entity.ToTable("term");

            entity.Property(e => e.TermId)
                .ValueGeneratedNever()
                .HasColumnName("term_id");
            entity.Property(e => e.Course).HasColumnName("course");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.TimeId).HasName("time_pk");

            entity.ToTable("time");

            entity.HasIndex(e => new { e.TypeId, e.LessonNumber }, "time_type_id_lesson_number_index").IsUnique();

            entity.Property(e => e.TimeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("time_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.End).HasColumnName("end");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LessonNumber).HasColumnName("lesson_number");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Times)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("time_type_id_fk");
        });

        modelBuilder.Entity<TimeType>(entity =>
        {
            entity.HasKey(e => e.TimeTypeId).HasName("time_type_pk");

            entity.ToTable("time_type");

            entity.HasIndex(e => e.Name, "time_type_name_index").IsUnique();

            entity.Property(e => e.TimeTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("time_type_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.TimetableId).HasName("timetable_pk");

            entity.ToTable("timetable");

            entity.HasIndex(e => new { e.Created, e.GroupId }, "timetable_created_group_id_index").IsUnique();

            entity.HasIndex(e => e.GroupId, "timetable_group_id_index").IsUnique();

            entity.Property(e => e.TimetableId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("timetable_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("now()")
                .HasColumnName("created");
            entity.Property(e => e.DayId).HasColumnName("day_id");
            entity.Property(e => e.Ended).HasColumnName("ended");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.WeekTypeId).HasColumnName("week_type_id");

            entity.HasOne(d => d.Day).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("timetable_day_id_fk");

            entity.HasOne(d => d.Group).WithOne(p => p.Timetable)
                .HasForeignKey<Timetable>(d => d.GroupId)
                .HasConstraintName("timetable_group_id_fk");

            entity.HasOne(d => d.WeekType).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.WeekTypeId)
                .HasConstraintName("timetable_week_type_id_fk");
        });

        modelBuilder.Entity<WeekType>(entity =>
        {
            entity.HasKey(e => e.WeekTypeId).HasName("week_type_pk");

            entity.ToTable("week_type");

            entity.HasIndex(e => e.Name, "week_type_name_index").IsUnique();

            entity.Property(e => e.WeekTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("week_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
