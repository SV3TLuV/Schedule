package com.example.schedulemobile.data.network.networkGroup

data class NetworkGrouped<TKey, TItem>(
    val key: TKey,
    val items: List<TItem>?
)