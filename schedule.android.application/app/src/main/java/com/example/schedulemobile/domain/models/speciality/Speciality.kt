package com.example.schedulemobile.domain.models.speciality

import com.example.schedulemobile.domain.models.discipline.Discipline

data class Speciality(
    val id: Int,
    val code: String,
    val name: String,
    val maxTermId: Int,
    val isDeleted: Boolean,
    val disciplines: List<Discipline>?
)
