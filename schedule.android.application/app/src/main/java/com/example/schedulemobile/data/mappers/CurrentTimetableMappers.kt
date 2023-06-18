package com.example.schedulemobile.data.mappers

import com.example.schedulemobile.data.network.networkClassroom.NetworkClassroom
import com.example.schedulemobile.data.network.networkCourse.NetworkCourse
import com.example.schedulemobile.data.network.networkCurrentTimetable.NetworkCurrentTimetable
import com.example.schedulemobile.data.network.networkCurrentTimetable.NetworkCurrentTimetableList
import com.example.schedulemobile.data.network.networkDate.NetworkDate
import com.example.schedulemobile.data.network.networkDay.NetworkDay
import com.example.schedulemobile.data.network.networkDiscipline.NetworkDiscipline
import com.example.schedulemobile.data.network.networkDisciplineType.NetworkDisciplineType
import com.example.schedulemobile.data.network.networkGroup.NetworkGroup
import com.example.schedulemobile.data.network.networkGroup.NetworkGroupList
import com.example.schedulemobile.data.network.networkGroup.NetworkGrouped
import com.example.schedulemobile.data.network.networkLesson.NetworkLesson
import com.example.schedulemobile.data.network.networkSpeciality.NetworkSpeciality
import com.example.schedulemobile.data.network.networkTeacher.NetworkTeacher
import com.example.schedulemobile.data.network.networkTeacherClassroom.NetworkTeacherClassroom
import com.example.schedulemobile.data.network.networkTerm.NetworkTerm
import com.example.schedulemobile.data.network.networkTime.NetworkTime
import com.example.schedulemobile.data.network.networkTimeType.NetworkTimeType
import com.example.schedulemobile.data.network.networkTimetable.NetworkTimetable
import com.example.schedulemobile.data.network.networkWeekType.NetworkWeekType
import com.example.schedulemobile.domain.models.classroom.Classroom
import com.example.schedulemobile.domain.models.course.Course
import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetable
import com.example.schedulemobile.domain.models.currentTimetable.CurrentTimetableList
import com.example.schedulemobile.domain.models.date.Date
import com.example.schedulemobile.domain.models.day.Day
import com.example.schedulemobile.domain.models.discipline.Discipline
import com.example.schedulemobile.domain.models.disciplineType.DisciplineType
import com.example.schedulemobile.domain.models.group.Group
import com.example.schedulemobile.domain.models.group.GroupList
import com.example.schedulemobile.domain.models.group.Grouped
import com.example.schedulemobile.domain.models.lesson.Lesson
import com.example.schedulemobile.domain.models.speciality.Speciality
import com.example.schedulemobile.domain.models.teacher.Teacher
import com.example.schedulemobile.domain.models.teacherClassroom.TeacherClassroom
import com.example.schedulemobile.domain.models.term.Term
import com.example.schedulemobile.domain.models.time.Time
import com.example.schedulemobile.domain.models.timeType.TimeType
import com.example.schedulemobile.domain.models.timetable.Timetable
import com.example.schedulemobile.domain.models.weekType.WeekType


fun NetworkCurrentTimetableList.toCurrentTimetableList(): CurrentTimetableList {
    return CurrentTimetableList(
        pageSize = pageSize,
        pageNumber = pageNumber,
        totalCount = totalCount,
        totalPages = totalPages,
        items = items?.map(NetworkCurrentTimetable::toCurrentTimetable)
    )
}

fun NetworkDay.toDay(): Day {
    return Day(
        id = id,
        name = name,
        isStudy = isStudy
    )
}

fun NetworkDiscipline.toDiscipline(): Discipline {
    return Discipline(
        id = id,
        name = name,
        code = code,
        totalHours = totalHours,
        isDeleted = isDeleted,
        disciplineType = disciplineType?.toDisciplineType(),
        term = term?.toTerm(),
        speciality = speciality?.toSpeciality()
    )
}

fun NetworkGroup.toGroup(): Group {
    return Group(
        id = id,
        number = number,
        name = name,
        enrollmentYear = enrollmentYear,
        isDeleted = isDeleted,
        term = term?.toTerm(),
        speciality = speciality.toSpeciality()
    )
}

fun NetworkWeekType.toWeekType(): WeekType {
    return WeekType(
        id = id,
        name = name
    )
}

fun NetworkGroupList.toGroupList(): GroupList {
    return GroupList(
        pageSize = pageSize,
        pageNumber = pageNumber,
        totalCount = totalCount,
        totalPages = totalPages,
        items = items.map(NetworkGroup::toGroup)
    )
}

fun NetworkGrouped<NetworkDate, NetworkTimetable>.toGroupedTimetable(): Grouped<Date, Timetable> {
    return Grouped(
        key = key.toDate(),
        items = items?.map(NetworkTimetable::toTimetable)
    )
}

fun NetworkLesson.toLesson(): Lesson {
    return Lesson(
        id = id,
        number = number,
        subgroup = subgroup,
        timetableId = timetableId,
        isChanged = isChanged,
        time = time?.toTime(),
        discipline = discipline?.toDiscipline(),
        teacherClassrooms = teacherClassrooms.map(NetworkTeacherClassroom::toTeacherClassroom)
    )
}

fun NetworkTime.toTime(): Time {
    return Time(
        id = id,
        start = start,
        end = end,
        lessonNumber = lessonNumber,
        type = type?.toTimeType(),
        isDeleted = isDeleted
    )
}

fun NetworkTeacherClassroom.toTeacherClassroom(): TeacherClassroom {
    return TeacherClassroom(
        teacher = teacher.toTeacher(),
        classroom = classroom?.toClassroom()
    )
}

fun NetworkTeacher.toTeacher(): Teacher {
    return Teacher(
        id = id,
        name = name,
        surname = surname,
        middleName = middleName,
        email = email
    )
}

fun NetworkClassroom.toClassroom(): Classroom {
    return Classroom(
        id = id,
        cabinet = cabinet
    )
}

fun NetworkTimetable.toTimetable(): Timetable {
    return Timetable(
        id = id,
        date = date.toDate(),
        groups = groups?.map(NetworkGroup::toGroup),
        lessons = lessons?.map(NetworkLesson::toLesson)
    )
}

fun NetworkDate.toDate(): Date {
    return Date(
        id = id,
        isStudy = isStudy,
        term = term,
        value = value,
        day = day.toDay(),
        timeType = timeType?.toTimeType(),
        weekType = weekType?.toWeekType()
    )
}

fun NetworkTimeType.toTimeType(): TimeType {
    return TimeType(
        id = id,
        name = name
    )
}

fun NetworkTerm.toTerm(): Term {
    return Term(
        id = id,
        courseTerm = courseTerm,
        course = course.toCourse()
    )
}

fun NetworkCourse.toCourse(): Course {
    return Course(
        id = id
    )
}

fun NetworkSpeciality.toSpeciality(): Speciality {
    return Speciality(
        id = id,
        code = code,
        name = name,
        maxTermId = maxTermId,
        isDeleted = isDeleted,
        disciplines = disciplines?.map(NetworkDiscipline::toDiscipline)
    )
}

fun NetworkCurrentTimetable.toCurrentTimetable(): CurrentTimetable {
    return CurrentTimetable(
        groups = groups?.map(NetworkGroup::toGroup),
        groupNames = groupNames,
        dates = dates?.map(NetworkGrouped<NetworkDate, NetworkTimetable>::toGroupedTimetable)
    )
}

fun NetworkDisciplineType.toDisciplineType(): DisciplineType {
    return DisciplineType(
        id = id,
        name = name
    )
}