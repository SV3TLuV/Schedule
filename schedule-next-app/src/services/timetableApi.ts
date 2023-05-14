import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "@/common/enums/HttpMethod";
import {IPagedList} from "@/features/models/IPagedList";
import {ITimetable} from "@/features/models/ITimetable";
import {IPaginatedQueryWithFilter} from "@/features/queries/IPaginatedQueryWithFilter";

export const timetableApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimetables: builder.query<IPagedList<ITimetable>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Timetable}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Timetable, id} as const)),
                {type: ApiTags.Timetable, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getTimetable: builder.query<ITimetable, number>({
            query: id => ({
                url: `${ApiTags.Timetable}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Timetable, id}
            ]
        }),
        getCurrentTimetable: builder.query<ITimetable, number>({
            query: () => ({
                url: `${ApiTags.Timetable}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: () => [
                {type: ApiTags.Timetable, id: "CURRENT"}
            ]
        }),
    }),
})

export const {
    useGetTimetablesQuery,
    useLazyGetTimetablesQuery,
    useGetTimetableQuery,
    useLazyGetTimetableQuery,
    useGetCurrentTimetableQuery,
    useLazyGetCurrentTimetableQuery,
} = timetableApi

export const {
    getTimetables,
    getTimetable,
    getCurrentTimetable,
} = timetableApi.endpoints