package com.example.schedulemobile.data.remote

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetable
import com.example.schedulemobile.domain.repository.CurrentTimetableRepository
import retrofit2.HttpException
import java.io.IOException

class TimetableSource(
    private val repository: CurrentTimetableRepository,
    private val groupId: Int
) : PagingSource<Int, CurrentTimetable>() {

    override fun getRefreshKey(state: PagingState<Int, CurrentTimetable>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, CurrentTimetable> {
        return try {
            val nextPage = params.key ?: 1
            val currentTimetableList = repository.getCurrentTimetableList(groupId)

            LoadResult.Page(
                data = currentTimetableList.data?.items ?: emptyList(),
                prevKey = null,//if (nextPage == 1) null else nextPage - 1,
                nextKey = 1//if (currentTimetableList.data?.items?.isEmpty() != false) null else currentTimetableList.data.pageNumber + 1
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

}