package com.example.schedulemobile.presentation.navigation

import androidx.activity.compose.BackHandler
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.schedulemobile.presentation.viewModels.GroupViewModel
import com.example.schedulemobile.presentation.viewModels.ScheduleViewModel
import com.example.schedulemobile.presentation.views.GroupView
import com.example.schedulemobile.presentation.views.ScheduleView

@Composable
fun MainNavigation(
    modifier: Modifier = Modifier,
    groupViewModel: GroupViewModel = hiltViewModel(),
    scheduleViewModel: ScheduleViewModel = hiltViewModel(),
    navController: NavHostController = rememberNavController(),
    startDestination: String,
) {
    NavHost(
        modifier = modifier,
        navController = navController,
        startDestination = startDestination
    ) {
        composable(NavRoute.GroupScreen.route) {
            BackHandler(true) {
            }
            GroupView(navController = navController, viewModel = groupViewModel)
        }

        composable(NavRoute.ScheduleScreen.route) {
            BackHandler(true) {
            }
            ScheduleView(navController = navController, viewModel = scheduleViewModel)
        }
    }
}

sealed class NavRoute(val route: String) {
    object GroupScreen: NavRoute("groupScreen")
    object ScheduleScreen: NavRoute("scheduleScreen")
    object SettingsScreen: NavRoute("settingsScreen")
}