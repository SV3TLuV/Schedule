import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export enum ApiTags {
    Classroom = "Classroom",
    ClassroomType = "ClassroomType",
    Date = "Date",
    Day = "Day",
    Discipline = "Discipline",
    Group = "Group",
    Lesson = "Lesson",
    SpecialityCode = "SpecialityCode",
    Teacher = "Teacher",
    Template = "Template",
    Time = "Time",
    Timetable = "Timetable",
    TimeType = "TimeType",
    WeekType = "WeekType",
}

export const baseApi = createApi({
    reducerPath: "BaseApi",
    baseQuery: fetchBaseQuery({
        baseUrl: "http://localhost:8080/api/",
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({}),
});