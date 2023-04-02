using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public string Cabinet { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; } = new List<LessonTeacherClassroom>();

    public virtual ICollection<ClassroomType> ClassroomTypes { get; } = new List<ClassroomType>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
