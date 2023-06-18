package com.example.schedulemobile.data.remote

import com.example.schedulemobile.data.network.networkGroup.NetworkGroupList
import retrofit2.http.GET
import retrofit2.http.Query

interface GroupApi {

    @GET("Group")
    suspend fun getGroupsByPage(
        @Query("Search") search: String?,
        @Query("Page") page: Int,
        @Query("PageSize") pageSize: Int = 100
    ): NetworkGroupList

    @GET("Group")
    suspend fun searchGroup(
        @Query("Search") search: String?,
        @Query("Page") page: Int = 1,
        @Query("PageSize") pageSize: Int = 100
    ) : NetworkGroupList
}