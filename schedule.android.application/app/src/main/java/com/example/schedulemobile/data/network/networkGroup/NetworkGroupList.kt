package com.example.schedulemobile.data.network.networkGroup

data class NetworkGroupList(
    val pageSize: Int,
    val pageNumber: Int,
    val totalCount: Int,
    val totalPages: Int,
    val items: List<NetworkGroup>
)
