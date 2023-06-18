package com.example.schedulemobile.presentation.customComponents

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.schedulemobile.domain.models.lesson.Lesson

@Composable
fun OutputLesson(
    modifier: Modifier = Modifier,
    lesson: Lesson?
) {
    val color = if (lesson?.isChanged == true) MaterialTheme.colorScheme.error else MaterialTheme.colorScheme.primary
    Column(
        modifier = modifier.padding(vertical = 10.dp)
    ) {
        DisciplineText(
            modifier = modifier
                .padding(vertical = 3.dp, horizontal = 5.dp),
                //.background(color, RoundedCornerShape(10.dp)),
            discipline = lesson?.discipline,
            color = color
        )
        if (lesson?.subgroup != null) {
            SubgroupsText(
                modifier = modifier
                    .padding(vertical = 3.dp, horizontal = 5.dp),
                //.background(color, RoundedCornerShape(10.dp)),
                lesson = lesson,
                color = color
            )
        }
        Row(
            modifier = Modifier
                .fillMaxWidth()
        ) {
            TeacherText(
                modifier = modifier
                    .padding(vertical = 3.dp, horizontal = 5.dp)
                    //.background(color, RoundedCornerShape(10.dp))
                    .weight(2f),
                teacher = lesson?.teacherClassrooms?.firstOrNull()?.teacher,
                color = color
            )
            CabinetText(
                modifier = modifier
                    .padding(vertical = 3.dp, horizontal = 5.dp)
                    //.background(color, RoundedCornerShape(10.dp))
                    .weight(1f),
                cabinet = lesson?.teacherClassrooms?.firstOrNull()?.classroom,
                color = color
            )
        }
        Row(
            modifier = Modifier
                .fillMaxWidth()
        ) {
            TeacherText(
                modifier = modifier
                    .padding(vertical = 3.dp, horizontal = 5.dp)
                    //.background(color, RoundedCornerShape(10.dp))
                    .weight(2f),
                teacher = if (lesson?.teacherClassrooms?.firstOrNull() != lesson?.teacherClassrooms?.lastOrNull()) lesson?.teacherClassrooms?.lastOrNull()?.teacher else null,
                color = color
            )
            CabinetText(
                modifier = modifier
                    .padding(vertical = 3.dp, horizontal = 5.dp)
                    //.background(color, RoundedCornerShape(10.dp))
                    .weight(1f),
                cabinet = if (lesson?.teacherClassrooms?.firstOrNull() != lesson?.teacherClassrooms?.lastOrNull()) lesson?.teacherClassrooms?.lastOrNull()?.classroom else null,
                color = color
            )
        }
        LessonTimeText(
            modifier = modifier
                .padding(vertical = 3.dp, horizontal = 5.dp),
                //.background(color, RoundedCornerShape(10.dp)),
            time = lesson?.time,
            color = color
        )
    }
}