import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export enum ApiTags {
    Classroom = "Classroom",
    ClassroomType = "ClassroomType",
    Date = "Date",
    Day = "Day",
    Discipline = "Discipline",
    Group = "Group",
    Lesson = "Lesson",
    Speciality = "Speciality",
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
        baseUrl: "https://localhost:7239/api/",
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({}),
})

export const buildUrlArguments = (params: object): string => {
    const urlParams = new URLSearchParams();
    for (const [key, value] of Object.entries(params)) {
        if (value !== undefined && value !== null) {
            urlParams.append(key, value.toString());
        }
    }
    return urlParams.toString();
}