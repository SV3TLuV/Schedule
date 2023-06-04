import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {IPagedList} from "../../features/models";
import {ITimetable} from "../../features/models";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {IGetCurrentTimetableQuery} from "../../features/queries";
import {IGetTimetableListQuery} from "../../features/queries";
import {ICurrentTimetable} from "../../features/models";
import {ApiTags} from "./apiTags.ts";

export const timetableApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimetables: builder.query<IPagedList<ITimetable>, IGetTimetableListQuery | void>({
            query: query => ({
                url: `${ApiTags.Timetable}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Timetable, id} as const)),
                {
                    type: ApiTags.Timetable,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
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
        getCurrentTimetable: builder.query<IPagedList<ICurrentTimetable>, IGetCurrentTimetableQuery | void>({
            query: query => ({
                url: `${ApiTags.Timetable}/current?${buildUrlArguments(query ?? {})}`,
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