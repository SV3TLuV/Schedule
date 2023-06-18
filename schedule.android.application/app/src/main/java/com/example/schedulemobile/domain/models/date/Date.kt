package com.example.schedulemobile.domain.models.date

import com.example.schedulemobile.domain.models.day.Day
import com.example.schedulemobile.domain.models.timeType.TimeType
import com.example.schedulemobile.domain.models.weekType.WeekType

data class Date(
    val id: Int,
    val isStudy: Boolean,
    val term: Int,
    val value: String,
    val day: Day,
    val timeType: TimeType?,
    val weekType: WeekType?
)
