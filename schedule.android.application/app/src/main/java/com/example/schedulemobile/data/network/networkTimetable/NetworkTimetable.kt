package com.example.schedulemobile.data.network.networkTimetable

import com.example.schedulemobile.data.network.networkDate.NetworkDate
import com.example.schedulemobile.data.network.networkGroup.NetworkGroup
import com.example.schedulemobile.data.network.networkLesson.NetworkLesson

data class NetworkTimetable(
    val id: Int,
    val date: NetworkDate,
    val groups: List<NetworkGroup>?,
    val lessons: List<NetworkLesson>?
)
