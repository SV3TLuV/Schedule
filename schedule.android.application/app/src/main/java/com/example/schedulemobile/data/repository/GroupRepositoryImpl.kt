package com.example.schedulemobile.data.repository

import com.example.schedulemobile.data.remote.GroupApi
import com.example.schedulemobile.data.mappers.toGroupList
import com.example.schedulemobile.domain.models.group.GroupList
import com.example.schedulemobile.domain.repository.GroupRepository
import com.example.schedulemobile.domain.util.Resource
import javax.inject.Inject

class GroupRepositoryImpl @Inject constructor(
    private val api: GroupApi
): GroupRepository {

    override suspend fun getGroupsByPage(search: String?, page: Int, pageSize: Int): Resource<GroupList> {
        return try {
            Resource.Success(
                data = api.getGroupsByPage(
                    search = search,
                    page = page,
                    pageSize = pageSize
                ).toGroupList()
            )
        } catch (e: Exception) {
            e.printStackTrace()
            Resource.Error(e.message ?: "Неизвестная ошибка.")
        }
    }

    override suspend fun searchGroup(
        search: String,
        page: Int,
        pageSize: Int
    ): Resource<GroupList> {
        return try {
            Resource.Success(
                data = api.searchGroup(
                    search = search,
                    page = page,
                    pageSize = pageSize
                ).toGroupList()
            )
        } catch (e: Exception) {
            e.printStackTrace()
            Resource.Error(e.message ?: "Неизвестная ошибка.")
        }
    }
}
