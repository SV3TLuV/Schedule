package com.example.schedulemobile.data.network.networkClassroom

data class NetworkClassroomList(
    val items: List<NetworkClassroom>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalCount: Int,
    val totalPages: Int
)