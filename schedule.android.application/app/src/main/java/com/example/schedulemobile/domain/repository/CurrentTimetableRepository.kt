package com.example.schedulemobile.domain.repository

import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetableList
import com.example.schedulemobile.domain.util.Resource

interface CurrentTimetableRepository {
    suspend fun getCurrentTimetableList(groupId: Int, dayCount: Int = 7, page: Int = 1, pageSize: Int = 100): Resource<CurrentTimetableList>
}