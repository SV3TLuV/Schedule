package com.example.schedulemobile.data.remote

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.schedulemobile.domain.models.group.Group
import com.example.schedulemobile.domain.repository.GroupRepository
import retrofit2.HttpException
import java.io.IOException

class GroupSource(
    private val repository: GroupRepository,
    private val search: String?
) : PagingSource<Int, Group>() {

    override fun getRefreshKey(state: PagingState<Int, Group>): Int? {
        return state.anchorPosition
    }

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Group> {
        return try {
            val nextPage = params.key ?: 1
            val groupList = repository.getGroupsByPage(
                search = if (search.isNullOrEmpty()) null else search,
                page = nextPage
            )

            LoadResult.Page(
                data = groupList.data?.items ?: emptyList(),
                prevKey = if (nextPage == 1) null else nextPage - 1,
                nextKey = if (groupList.data?.items?.isEmpty() != false) null else groupList.data.pageNumber + 1
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }
}