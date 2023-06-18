package com.example.schedulemobile.data.network.networkTeacherClassroom

import com.example.schedulemobile.data.network.networkClassroom.NetworkClassroom
import com.example.schedulemobile.data.network.networkTeacher.NetworkTeacher

data class NetworkTeacherClassroom(
    val teacher: NetworkTeacher,
    val classroom: NetworkClassroom?
)
