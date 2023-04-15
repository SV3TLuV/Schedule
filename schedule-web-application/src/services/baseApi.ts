import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {IPagedList} from "../features/models/IPagedList";
import {IGroup} from "../features/models/IGroup";
import {HttpMethod} from "../common/enums/HttpMethod";

export enum ApiTags {
    Classroom = "CLASSROOM",
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
        baseUrl: "https://localhost:7239/api/",
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({}),
});

export const groupApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getGroups: builder.query<IPagedList<IGroup>, void>({
            query: () => ({
                url: ApiTags.Group,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({ id }) => ({ type: ApiTags.Group, id } as const)),
                { type: ApiTags.Group, id: "LIST", page: result?.pageNumber }
            ]
        })
    }),
});