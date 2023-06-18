package com.example.schedulemobile.domain.models.group

import com.example.schedulemobile.domain.models.speciality.Speciality
import com.example.schedulemobile.domain.models.term.Term

data class Group(
    val id: Int,
    val number: String,
    val name: String,
    val enrollmentYear: Int,
    val isDeleted: Boolean,
    val term: Term?,
    val speciality: Speciality,
    //val mergedGroups: List<Group>?
)
