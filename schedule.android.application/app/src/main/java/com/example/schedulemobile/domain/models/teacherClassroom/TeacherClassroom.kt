package com.example.schedulemobile.domain.models.teacherClassroom

import com.example.schedulemobile.domain.models.classroom.Classroom
import com.example.schedulemobile.domain.models.teacher.Teacher

data class TeacherClassroom(
    val teacher: Teacher,
    val classroom: Classroom?
)
