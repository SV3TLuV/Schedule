package com.example.schedulemobile.domain.models.group

data class Grouped<TKey, TItem>(
    val key: TKey,
    val items: List<TItem>?
)