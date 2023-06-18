package com.example.schedulemobile.data.network.networkSpeciality

data class NetworkSpecialityCodeData(
    val items: List<NetworkSpeciality>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalCount: Int,
    val totalPages: Int
)
