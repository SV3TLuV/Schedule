package com.example.schedulemobile.presentation.views

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.heightIn
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.ExperimentalMaterialApi
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Search
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.Divider
import androidx.compose.material3.DockedSearchBar
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.ListItem
import androidx.compose.material3.Scaffold
import androidx.compose.material3.SnackbarHost
import androidx.compose.material3.SnackbarHostState
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewModelScope
import androidx.navigation.NavController
import androidx.paging.LoadState
import androidx.paging.compose.collectAsLazyPagingItems
import androidx.paging.compose.itemContentType
import androidx.paging.compose.itemKey
import com.example.schedulemobile.presentation.navigation.NavRoute
import com.example.schedulemobile.presentation.viewModels.GroupViewModel
import kotlinx.coroutines.launch

@OptIn(ExperimentalMaterial3Api::class, ExperimentalMaterialApi::class)
@Composable
fun GroupView(
    navController: NavController,
    viewModel: GroupViewModel
) {

    val snackBarHostState = remember { SnackbarHostState() }
    val isSnackBarShowing = viewModel.isSnackBarShowing
    val focusManager = LocalFocusManager.current
    val groups = viewModel.groups.collectAsLazyPagingItems()

    LaunchedEffect(isSnackBarShowing) {
        if (isSnackBarShowing) {
            snackBarHostState.showSnackbar("Установлена некорректная группа")
            viewModel.isSnackBarShowing = false
        }
    }
    var active by rememberSaveable { mutableStateOf(false) }
    var searchQuery by rememberSaveable { mutableStateOf("") }

    fun closeSearchBar() {
        active = false
        focusManager.clearFocus()
    }

    Scaffold(
        snackbarHost = { SnackbarHost(snackBarHostState) },
        modifier = Modifier
            .fillMaxSize()
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            // Элемент выбора группы из списка
            DockedSearchBar(
                modifier = Modifier
                    .heightIn(20.dp)
                    .padding(top = 8.dp)
                    .padding(horizontal = 15.dp),
                query = searchQuery,
                onQueryChange = { query ->
                    searchQuery = query
                    viewModel.setSearchQuery(query)
                },
                onSearch = { closeSearchBar() },
                active = active,
                onActiveChange = {
                    active = it
                    if (!active) focusManager.clearFocus()
                },
                placeholder = { Text("Группа") },
                leadingIcon = {
                    Icon(
                        Icons.Default.Search,
                        contentDescription = null,
                        modifier = Modifier.clickable { closeSearchBar() }
                    )
                },
            ) {
                // Проверка, загружены ли данные из API
                if (groups.loadState.refresh is LoadState.Loading) {
                    Box(modifier = Modifier.fillMaxSize()) {
                        // Элемент загрузки
                        CircularProgressIndicator(
                            modifier = Modifier.align(Alignment.Center)
                        )
                    }
                } else {
                    // Список с группами
                    LazyColumn {
                        items(
                            count = groups.itemCount,
                            key = groups.itemKey(),
                            contentType = groups.itemContentType(
                            )
                        ) { index ->
                            val item = groups[index]
                            if (item != null) {
                                // Элемент списка
                                ListItem(
                                    headlineContent = { Text(item.name) },
                                    modifier = Modifier.clickable {
                                        searchQuery = item.name
                                        closeSearchBar()
                                    }
                                )
                                Divider()
                            }
                        }
                    }
                }
            }

            Button(
                modifier = Modifier
                    .padding(top = 10.dp),
                onClick = {
                    viewModel.viewModelScope.launch {
                        focusManager.clearFocus()

                        if (viewModel.validGroup(searchQuery)) navController.navigate(NavRoute.ScheduleScreen.route)
                        else viewModel.isSnackBarShowing = true
                    }
                },
                content = {
                    Text(text = "Сохранить")
                }
            )
        }
    }
}