package com.example.schedulemobile.data.network.networkLessonTeacher

import com.example.schedulemobile.data.network.networkClassroom.NetworkClassroom
import com.example.schedulemobile.data.network.networkLesson.NetworkLesson
import com.example.schedulemobile.data.network.networkTeacher.NetworkTeacher

data class NetworkLessonTeacher(
    val lesson: NetworkLesson,
    val teacher: NetworkTeacher,
    val classroom: NetworkClassroom
)
