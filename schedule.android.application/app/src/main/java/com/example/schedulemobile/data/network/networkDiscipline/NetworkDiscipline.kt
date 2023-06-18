package com.example.schedulemobile.data.network.networkDiscipline

import com.example.schedulemobile.data.network.networkDisciplineType.NetworkDisciplineType
import com.example.schedulemobile.data.network.networkSpeciality.NetworkSpeciality
import com.example.schedulemobile.data.network.networkTerm.NetworkTerm

data class NetworkDiscipline(
    val id: Int,
    val name: String,
    val code: String,
    val totalHours: Int,
    val isDeleted: Boolean,
    val disciplineType: NetworkDisciplineType?,
    val term: NetworkTerm?,
    val speciality: NetworkSpeciality?
)
