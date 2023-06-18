package com.example.schedulemobile.data.network.networkDate

import com.example.schedulemobile.data.network.networkDay.NetworkDay
import com.example.schedulemobile.data.network.networkTimeType.NetworkTimeType
import com.example.schedulemobile.data.network.networkWeekType.NetworkWeekType

data class NetworkDate(
    val id: Int,
    val isStudy: Boolean,
    val term: Int,
    val value: String,
    val day: NetworkDay,
    val timeType: NetworkTimeType?,
    val weekType: NetworkWeekType?
)
