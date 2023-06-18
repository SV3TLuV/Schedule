package com.example.schedulemobile.domain.models.group

data class GroupList(
    val pageSize: Int,
    val pageNumber: Int,
    val totalCount: Int,
    val totalPages: Int,
    val items: List<Group>
)
