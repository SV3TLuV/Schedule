package com.example.schedulemobile.domain.util

import com.example.schedulemobile.domain.models.teacher.Teacher

fun getShortFullName(teacher: Teacher?): String {
    return if (teacher == null) "" else "${teacher.surname} ${teacher.name[0]}.${teacher.middleName[0]}."
}