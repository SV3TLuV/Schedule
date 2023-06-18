package com.example.schedulemobile.data.network.networkCurrentTimetable

data class NetworkCurrentTimetableList(
    val pageSize: Int,
    val pageNumber: Int,
    val totalCount: Int,
    val totalPages: Int,
    val items: List<NetworkCurrentTimetable>?
)
