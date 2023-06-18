package com.example.schedulemobile.data.network.networkTime

import com.example.schedulemobile.data.network.networkTimeType.NetworkTimeType

data class NetworkTime(
    val id: Int,
    val start: String,
    val end: String,
    val lessonNumber: Int,
    val type: NetworkTimeType?,
    val isDeleted: Boolean
)
