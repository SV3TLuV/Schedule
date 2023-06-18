package com.example.schedulemobile.presentation.views

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.border
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.pager.HorizontalPager
import androidx.compose.foundation.pager.rememberPagerState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Card
import androidx.compose.material3.CenterAlignedTopAppBar
import androidx.compose.material3.DrawerValue
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.ModalDrawerSheet
import androidx.compose.material3.ModalNavigationDrawer
import androidx.compose.material3.NavigationDrawerItem
import androidx.compose.material3.NavigationDrawerItemDefaults
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.rememberDrawerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalLifecycleOwner
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavController
import com.example.schedulemobile.R
import com.example.schedulemobile.presentation.customComponents.OutputLesson
import com.example.schedulemobile.presentation.navigation.NavRoute
import com.example.schedulemobile.presentation.viewModels.ObserverLifecycle
import com.example.schedulemobile.presentation.viewModels.ScheduleViewModel
import com.google.accompanist.swiperefresh.SwipeRefresh
import com.google.accompanist.swiperefresh.rememberSwipeRefreshState
import kotlinx.coroutines.launch

@OptIn(
    ExperimentalMaterial3Api::class,
    ExperimentalFoundationApi::class
)
@Composable
fun ScheduleView(
    navController: NavController,
    viewModel: ScheduleViewModel
) {

    val lifecycleOwner = LocalLifecycleOwner.current
    viewModel.ObserverLifecycle(lifecycle = lifecycleOwner.lifecycle)

    val isLoading = viewModel.state.isLoading
    val isRefreshing by viewModel.isRefreshing.collectAsState()

    val pagerState = rememberPagerState()
    val scope = rememberCoroutineScope()
    val drawerState = rememberDrawerState(DrawerValue.Closed)
    val items = DrawerItems.values()

    val currentTimetableList = viewModel.state.currentTimetableList?.items?.firstOrNull()

    SwipeRefresh(
        state = rememberSwipeRefreshState(isRefreshing = isRefreshing),
        onRefresh = { viewModel.refresh(lifecycleOwner) }
    ) {
        ModalNavigationDrawer(
            drawerState = drawerState,
            drawerContent = {
                ModalDrawerSheet {
                    items.forEach {
                        Spacer(Modifier.height(12.dp))
                        NavigationDrawerItem(
                            icon = { Icon(painterResource(it.icon), contentDescription = null) },
                            label = { Text(it.itemName) },
                            selected = false,
                            onClick = {
                                scope.launch { drawerState.close() }
                                navController.navigate(it.route)
                            },
                            modifier = Modifier.padding(NavigationDrawerItemDefaults.ItemPadding)
                        )
                    }
                }
            }
        ) {
            Scaffold(
                modifier = Modifier
                    .fillMaxSize(),
                topBar = {
                    CenterAlignedTopAppBar(
                        modifier = Modifier,
                        title = {
                            Text(
                                text = currentTimetableList?.groupNames ?: "",
                                modifier = Modifier.fillMaxWidth(),
                                textAlign = TextAlign.Center,
                                fontWeight = FontWeight.Black
                            )
                        },
                        navigationIcon = {
                            Icon(
                                painterResource(R.drawable.menu),
                                contentDescription = "",
                                modifier = Modifier
                                    .padding(start = 10.dp)
                                    .clickable {
                                        scope.launch { drawerState.open() }
                                    }
                            )
                        },
                    )
                },
                content = { paddingValues ->
                    if (isLoading) {
                        LoadingView()
                    } else {
                        HorizontalPager(
                            state = pagerState,
                            pageCount = currentTimetableList?.dates?.size ?: 0,
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(paddingValues),
                            userScrollEnabled = true
                        ) { pageIndex ->
                            val currentDay = currentTimetableList?.dates?.get(pageIndex)?.key?.day?.name

                            Column(
                                modifier = Modifier
                                    .padding(horizontal = 20.dp, vertical = 15.dp)
                                    .border(
                                        width = 2.dp,
                                        MaterialTheme.colorScheme.primary,
                                        RoundedCornerShape(15.dp)
                                    )
                            ) {
                                Text(
                                    modifier = Modifier
                                        .fillMaxWidth()
                                        .padding(top = 10.dp),
                                    text = currentDay ?: "",
                                    textAlign = TextAlign.Center,
                                    fontSize = 25.sp,
                                    fontWeight = FontWeight(600)
                                )
                                Card(
                                    modifier = Modifier
                                        .padding(vertical = 15.dp, horizontal = 15.dp)
                                ) {
                                    LazyColumn {
                                        items(
                                            items = currentTimetableList?.dates?.get(pageIndex)?.items
                                                ?: emptyList()
                                        ) { timetable ->
                                            timetable.lessons?.map { lesson ->
                                                OutputLesson(lesson = lesson)
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            )
        }
    }
}

enum class DrawerItems {
    GROUP {
        override val itemName = "Группа"
        override val icon = R.drawable.group
        override val route = NavRoute.GroupScreen.route
    }/*,
    SETTINGS {
        override val itemName = "Настройки"
        override val icon = R.drawable.settings
        override val route = NavRoute.SettingsScreen.route
    }*/;

    abstract val itemName: String
    abstract val icon: Int
    abstract val route: String
}