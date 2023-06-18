package com.example.schedulemobile.di

import com.example.schedulemobile.data.repository.CurrentTimetableRepositoryImpl
import com.example.schedulemobile.data.repository.GroupRepositoryImpl
import com.example.schedulemobile.domain.repository.CurrentTimetableRepository
import com.example.schedulemobile.domain.repository.GroupRepository
import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import kotlinx.coroutines.ExperimentalCoroutinesApi
import javax.inject.Singleton

@ExperimentalCoroutinesApi
@Module
@InstallIn(SingletonComponent::class)
abstract class RepositoryModule {

    @Binds
    @Singleton
    abstract fun bindCurrentTimetableRepository(
        currentTimetableRepositoryImpl: CurrentTimetableRepositoryImpl
    ): CurrentTimetableRepository

    @Binds
    @Singleton
    abstract fun bindGroupRepository(
        groupRepositoryImpl: GroupRepositoryImpl
    ): GroupRepository
}