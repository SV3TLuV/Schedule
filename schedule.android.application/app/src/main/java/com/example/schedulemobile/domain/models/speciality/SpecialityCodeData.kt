package com.example.schedulemobile.domain.models.speciality

data class SpecialityCodeData(
    val items: List<Speciality>,
    val pageNumber: Int,
    val pageSize: Int,
    val totalCount: Int,
    val totalPages: Int
)
