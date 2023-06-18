package com.example.schedulemobile.domain.repository

import com.example.schedulemobile.domain.models.group.GroupList
import com.example.schedulemobile.domain.util.Resource

interface GroupRepository {
    suspend fun getGroupsByPage(search: String?, page: Int, pageSize: Int = 100): Resource<GroupList>

    suspend fun searchGroup(search: String, page: Int = 1, pageSize: Int = 100): Resource<GroupList>
}