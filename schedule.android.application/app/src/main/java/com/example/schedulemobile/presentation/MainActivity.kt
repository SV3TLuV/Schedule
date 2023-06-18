package com.example.schedulemobile.presentation

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.viewModels
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import com.example.schedulemobile.presentation.navigation.MainNavigation
import com.example.schedulemobile.presentation.navigation.NavRoute
import com.example.schedulemobile.presentation.ui.theme.ScheduleMobileTheme
import com.example.schedulemobile.presentation.viewModels.GroupViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class MainActivity : ComponentActivity() {

    private val groupViewModel: GroupViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContent {
            ScheduleMobileTheme {
                Scaffold(
                    Modifier.fillMaxSize(),
                ) { paddingValues ->
                    var startDestination = NavRoute.GroupScreen.route
                    if (groupViewModel.getSavedGroup() != null) startDestination = NavRoute.ScheduleScreen.route
                    MainNavigation(
                        modifier = Modifier
                            .padding(paddingValues),
                        startDestination = startDestination
                    )
                }
            }
        }
    }
}