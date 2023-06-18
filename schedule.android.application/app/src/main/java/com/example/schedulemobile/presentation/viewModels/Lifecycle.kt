package com.example.schedulemobile.presentation.viewModels

import androidx.compose.runtime.Composable
import androidx.compose.runtime.DisposableEffect
import androidx.lifecycle.Lifecycle
import androidx.lifecycle.LifecycleObserver

@Composable
fun <LO : LifecycleObserver> LO.ObserverLifecycle(lifecycle: Lifecycle) {
    DisposableEffect(lifecycle) {
        lifecycle.addObserver(this@ObserverLifecycle)
        onDispose {
            lifecycle.removeObserver(this@ObserverLifecycle)
        }
    }
}