package com.example.schedulemobile.data.network.networkSpeciality

import com.example.schedulemobile.data.network.networkDiscipline.NetworkDiscipline

data class NetworkSpeciality(
    val id: Int,
    val code: String,
    val name: String,
    val maxTermId: Int,
    val isDeleted: Boolean,
    val disciplines: List<NetworkDiscipline>?
)
