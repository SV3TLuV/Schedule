package com.example.schedulemobile.presentation.customComponents

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.SolidColor
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.schedulemobile.domain.models.classroom.Classroom

@Composable
fun CabinetText(
    modifier: Modifier = Modifier,
    cabinet: Classroom?,
    color: Color
) {
    Column(
        modifier = modifier
            .height(40.dp)
            .border(width = 2.dp,
                brush = SolidColor(color),
                shape = RoundedCornerShape(10)
            ),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            text = cabinet?.cabinet ?: "",
            style = TextStyle(
                color = MaterialTheme.colorScheme.onSurface,
                fontSize = 16.sp,
                fontWeight = FontWeight(500),
            )
        )
    }
}