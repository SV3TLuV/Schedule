package com.example.schedulemobile.presentation

import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetableList

data class CurrentTimetableState(
    val currentTimetableList: CurrentTimetableList? = null,
    val isLoading: Boolean = false,
    val error: String? = null
)
