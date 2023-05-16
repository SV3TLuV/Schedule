import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ITimetable} from "../../features/models/ITimetable.ts";
import {IPaginatedQueryWithFilters} from "../../features/queries/IPaginatedQueryWithFilters.ts";

export const timetableApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimetables: builder.query<IPagedList<ITimetable>, IPaginatedQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Timetable}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Timetable, id} as const)),
                {type: ApiTags.Timetable, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getTimetable: builder.query<ITimetable, number>({
            query: id => ({
                url: `${ApiTags.Timetable}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.Timetable, id}
            ]
        }),
        getCurrentTimetable: builder.query<ITimetable, number>({
            query: () => ({
                url: `${ApiTags.Timetable}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: () => [
                {type: ApiTags.Timetable, id: 'CURRENT'}
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