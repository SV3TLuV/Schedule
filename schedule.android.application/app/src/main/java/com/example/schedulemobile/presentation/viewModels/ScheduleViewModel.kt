package com.example.schedulemobile.presentation.viewModels

import android.content.SharedPreferences
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.DefaultLifecycleObserver
import androidx.lifecycle.LifecycleOwner
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.schedulemobile.domain.models.group.Group
import com.example.schedulemobile.domain.repository.CurrentTimetableRepository
import com.example.schedulemobile.domain.util.Resource
import com.example.schedulemobile.presentation.CurrentTimetableState
import com.google.gson.Gson
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ScheduleViewModel @Inject constructor(
    private val repository: CurrentTimetableRepository,
    private val sharedPreferences: SharedPreferences,
    private val signalRClientViewModel: SignalRClient
): ViewModel(), DefaultLifecycleObserver {

    private val _isRefreshing = MutableStateFlow(false)
    private val _groupId = getCurrentGroup()?.id

    init {
        observeSignalREvents()
    }

    private fun observeSignalREvents() {
        signalRClientViewModel.hubConnection.on("notified", { objName ->
            if (objName == "Lesson") getCurrentTimetable()
        }, Any::class.java)
    }

    var state by mutableStateOf(CurrentTimetableState())
        private set

    val isRefreshing: StateFlow<Boolean>
        get() = _isRefreshing.asStateFlow()

    override fun onResume(owner: LifecycleOwner) {
        getCurrentTimetable()
    }

    private fun getCurrentTimetable() {
        viewModelScope.launch {
            state = state.copy(
                isLoading = true,
                error = null
            )
            try {
                when (val result = repository.getCurrentTimetableList(getCurrentGroup()?.id ?: 1)) {
                    is Resource.Success -> {
                        state = state.copy(
                            currentTimetableList = result.data,
                            isLoading = false,
                            error = null
                        )
                    }
                    is Resource.Error -> {
                        state = state.copy(
                            currentTimetableList = null,
                            isLoading = false,
                            error = result.message
                        )
                    }
                }
            } catch (e: Exception) {
                state = state.copy(
                    currentTimetableList = null,
                    isLoading = false,
                    error = "Ошибка при загрузке данных"
                )
            }
        }
    }

    fun refresh(lifecycle: LifecycleOwner) {
        viewModelScope.launch {
            _isRefreshing.emit(true)
            onResume(lifecycle)
            _isRefreshing.emit(false)
        }
    }

    private fun getCurrentGroup(): Group? {
        val jsonGroup = sharedPreferences.getString("group", null)
        return Gson().fromJson(jsonGroup, Group::class.java)
    }
}