﻿using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Entities;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; } = new List<LessonTeacherClassroom>();

    public virtual ICollection<Classroom> Classrooms { get; } = new List<Classroom>();

    public virtual ICollection<Discipline> Disciplines { get; } = new List<Discipline>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
