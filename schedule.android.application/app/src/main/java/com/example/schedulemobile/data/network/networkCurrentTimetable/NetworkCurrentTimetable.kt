package com.example.schedulemobile.data.network.networkCurrentTimetable

import com.example.schedulemobile.data.network.networkDate.NetworkDate
import com.example.schedulemobile.data.network.networkGroup.NetworkGroup
import com.example.schedulemobile.data.network.networkGroup.NetworkGrouped
import com.example.schedulemobile.data.network.networkTimetable.NetworkTimetable

data class NetworkCurrentTimetable(
    val groups: List<NetworkGroup>?,
    val groupNames: String?,
    val dates: List<NetworkGrouped<NetworkDate, NetworkTimetable>>?
)
