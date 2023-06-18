package com.example.schedulemobile.domain.models.lesson

import com.example.schedulemobile.domain.models.discipline.Discipline
import com.example.schedulemobile.domain.models.teacherClassroom.TeacherClassroom
import com.example.schedulemobile.domain.models.time.Time

data class Lesson(
    val id: Int,
    val number: Int,
    val subgroup: Int?,
    val timetableId: Int,
    val isChanged: Boolean,
    val time: Time?,
    val discipline: Discipline?,
    val teacherClassrooms: List<TeacherClassroom>
)
