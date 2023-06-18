package com.example.schedulemobile.data.repository

import com.example.schedulemobile.data.remote.TimetableApi
import com.example.schedulemobile.data.mappers.toCurrentTimetableList
import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetableList
import com.example.schedulemobile.domain.repository.CurrentTimetableRepository
import com.example.schedulemobile.domain.util.Resource
import javax.inject.Inject

class CurrentTimetableRepositoryImpl @Inject constructor(
    private val api: TimetableApi
): CurrentTimetableRepository {

    override suspend fun getCurrentTimetableList(
        groupId: Int,
        dayCount: Int,
        page: Int,
        pageSize: Int
    ): Resource<CurrentTimetableList> {
        return try {
            Resource.Success(
                data = api.getCurrentTimetableList(
                    groupId = groupId,
                    dayCount = dayCount,
                    page = page,
                    pageSize = pageSize
                ).toCurrentTimetableList()
            )
        } catch (e: Exception) {
            e.printStackTrace()
            Resource.Error(e.message ?: "Неизвестная ошибка.")
        }
    }
}