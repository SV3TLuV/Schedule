﻿namespace Schedule.Persistence.Entities;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int? TimeId { get; set; }

    public int TimetableId { get; set; }

    public int? DisciplineId { get; set; }

    public bool IsChanged { get; set; }

    public virtual Discipline? Discipline { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; } = new List<LessonTeacherClassroom>();

    public virtual Time? Time { get; set; }

    public virtual Template Timetable { get; set; } = null!;

    public virtual Timetable TimetableNavigation { get; set; } = null!;
}
