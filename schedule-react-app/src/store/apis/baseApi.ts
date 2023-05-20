import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export enum ApiTags {
    Classroom = 'Classroom',
    ClassroomType = 'ClassroomType',
    Date = 'Date',
    Day = 'Day',
    Discipline = 'Discipline',
    DisciplineType = 'DisciplineType',
    Group = 'Group',
    Lesson = 'Lesson',
    Speciality = 'Speciality',
    Teacher = 'Teacher',
    Template = 'Template',
    Time = 'Time',
    Timetable = 'Timetable',
    TimeType = 'TimeType',
    Term = 'Term',
    WeekType = 'WeekType',
}

export const baseApi = createApi({
    reducerPath: 'BaseApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'https://localhost:7239/api/',
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: () => ({}),
})