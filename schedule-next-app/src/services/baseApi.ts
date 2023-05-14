import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {HYDRATE} from "next-redux-wrapper";

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
    extractRehydrationInfo(action, { reducerPath }) {
        if (action.type === HYDRATE) {
            return action.payload[reducerPath]
        }
    },
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: () => ({}),
})

export const buildUrlArguments = (params: object): string => {
    if (!params) {
        return "";
    }

    const urlParams = new URLSearchParams();
    for (const [key, value] of Object.entries(params)) {
        if (value !== undefined && value !== null) {
            urlParams.append(key, value.toString());
        }
    }
    return urlParams.toString();
}