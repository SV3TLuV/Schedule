package com.example.schedulemobile.domain.models.lessonTeacher

import com.example.schedulemobile.domain.models.classroom.Classroom
import com.example.schedulemobile.domain.models.lesson.Lesson
import com.example.schedulemobile.domain.models.teacher.Teacher

data class LessonTeacher(
    val lesson: Lesson,
    val teacher: Teacher,
    val classroom: Classroom
)
