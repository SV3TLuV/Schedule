package com.example.schedulemobile.domain.models.discipline

import com.example.schedulemobile.domain.models.disciplineType.DisciplineType
import com.example.schedulemobile.domain.models.speciality.Speciality
import com.example.schedulemobile.domain.models.term.Term

data class Discipline(
    val id: Int,
    val name: String,
    val code: String,
    val totalHours: Int,
    val isDeleted: Boolean,
    val disciplineType: DisciplineType?,
    val term: Term?,
    val speciality: Speciality?
)
