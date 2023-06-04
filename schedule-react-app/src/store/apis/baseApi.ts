import {createApi} from "@reduxjs/toolkit/query/react";
import {fetchQueryWithReauth} from "../fetchBaseQueryWithReauth.ts";

export enum ApiTags {
    Classroom = 'Classroom',
    ClassroomType = 'ClassroomType',
    Course = 'Course',
    Date = 'Date',
    Day = 'Day',
    Discipline = 'Discipline',
    DisciplineType = 'DisciplineType',
    Group = 'Group',
    Lesson = 'Lesson',
    LessonNumber = 'LessonNumber',
    LessonTemplate = 'LessonTemplate',
    Roles = 'Role',
    Speciality = 'Speciality',
    Teacher = 'Teacher',
    Template = 'Template',
    Term = 'Term',
    Time = 'Time',
    Timetable = 'Timetable',
    TimeType = 'TimeType',
    Users = 'User',
    WeekType = 'WeekType',
}

export const baseApi = createApi({
    reducerPath: 'BaseApi',
    baseQuery: fetchQueryWithReauth,
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: () => ({}),
})