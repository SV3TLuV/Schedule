package com.example.schedulemobile.data.network.networkGroup

import com.example.schedulemobile.data.network.networkSpeciality.NetworkSpeciality
import com.example.schedulemobile.data.network.networkTerm.NetworkTerm

data class NetworkGroup(
    val id: Int,
    val number: String,
    val name: String,
    val enrollmentYear: Int,
    val isDeleted: Boolean,
    val term: NetworkTerm?,
    val speciality: NetworkSpeciality,
    //val mergedGroups: List<NetworkGroup>?
)
