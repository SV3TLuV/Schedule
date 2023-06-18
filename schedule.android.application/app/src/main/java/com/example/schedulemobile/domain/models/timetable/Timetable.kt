package com.example.schedulemobile.domain.models.timetable

import com.example.schedulemobile.domain.models.date.Date
import com.example.schedulemobile.domain.models.group.Group
import com.example.schedulemobile.domain.models.lesson.Lesson

data class Timetable(
    val id: Int,
    val date: Date,
    val groups: List<Group>?,
    val lessons: List<Lesson>?
)
