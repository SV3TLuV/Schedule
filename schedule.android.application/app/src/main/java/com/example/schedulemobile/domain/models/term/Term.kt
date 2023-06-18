package com.example.schedulemobile.domain.models.term

import com.example.schedulemobile.domain.models.course.Course

data class Term(
    val id: Int,
    val courseTerm: Int,
    val course: Course
)
