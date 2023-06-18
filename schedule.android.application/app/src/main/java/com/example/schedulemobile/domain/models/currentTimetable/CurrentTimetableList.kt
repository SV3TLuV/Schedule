package com.example.schedulemobile.domain.models.currentTimetable

data class CurrentTimetableList(
    val pageSize: Int,
    val pageNumber: Int,
    val totalCount: Int,
    val totalPages: Int,
    val items: List<CurrentTimetable>?
)
