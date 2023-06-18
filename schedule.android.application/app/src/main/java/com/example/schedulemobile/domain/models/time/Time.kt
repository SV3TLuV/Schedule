package com.example.schedulemobile.domain.models.time

import com.example.schedulemobile.domain.models.timeType.TimeType

data class Time(
    val id: Int,
    val start: String,
    val end: String,
    val lessonNumber: Int,
    val type: TimeType?,
    val isDeleted: Boolean
)
