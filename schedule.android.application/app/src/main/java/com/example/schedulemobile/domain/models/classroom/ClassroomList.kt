package com.example.schedulemobile.domain.models.classroom

data class ClassroomList(
    val items: List<Classroom>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalCount: Int,
    val totalPages: Int
)