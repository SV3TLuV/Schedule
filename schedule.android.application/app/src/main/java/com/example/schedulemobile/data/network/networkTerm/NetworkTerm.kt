package com.example.schedulemobile.data.network.networkTerm

import com.example.schedulemobile.data.network.networkCourse.NetworkCourse

data class NetworkTerm(
    val id: Int,
    val courseTerm: Int,
    val course: NetworkCourse
)
