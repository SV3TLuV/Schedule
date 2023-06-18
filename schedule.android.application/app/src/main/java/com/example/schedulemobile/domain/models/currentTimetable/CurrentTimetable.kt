package com.example.schedulemobile.domain.models.currentTimetable

import com.example.schedulemobile.domain.models.date.Date
import com.example.schedulemobile.domain.models.group.Group
import com.example.schedulemobile.domain.models.group.Grouped
import com.example.schedulemobile.domain.models.timetable.Timetable

data class CurrentTimetable(
    val groups: List<Group>?,
    val groupNames: String?,
    val dates: List<Grouped<Date, Timetable>>?
)
