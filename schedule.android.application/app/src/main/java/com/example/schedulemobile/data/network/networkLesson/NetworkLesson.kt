package com.example.schedulemobile.data.network.networkLesson

import com.example.schedulemobile.data.network.networkDiscipline.NetworkDiscipline
import com.example.schedulemobile.data.network.networkTeacherClassroom.NetworkTeacherClassroom
import com.example.schedulemobile.data.network.networkTime.NetworkTime

data class NetworkLesson(
    val id: Int,
    val number: Int,
    val subgroup: Int?,
    val timetableId: Int,
    val isChanged: Boolean,
    val time: NetworkTime?,
    val discipline: NetworkDiscipline?,
    val teacherClassrooms: List<NetworkTeacherClassroom>
)
