import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IDay} from "../../features/models/IDay.ts";
import {IPaginatedQueryWithFilter} from "../../features/queries/IPaginatedQueryWithFilter.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";

export const dayApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDays: builder.query<IPagedList<IDay>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Day}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Day, id} as const)),
                {type: ApiTags.Day, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getDay: builder.query<IDay, number>({
            query: id => ({
                url: `${ApiTags.Day}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [{type: ApiTags.Day, id}]
        }),
        getCurrentDay: builder.query<IDay, void>({
            query: () => ({
                url: `${ApiTags.Day}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                {type: ApiTags.Day, id: "CURRENT"},
                {type: ApiTags.Day, id: result?.id}
            ]
        }),
        updateDay: builder.mutation<number, IDay>({
            query: day => ({
                url: ApiTags.Day,
                method: HttpMethod.PUT,
                body: {
                    id: day.id,
                    isStudy: day.isStudy,
                },
            }),
            invalidatesTags: id => [
                {type: ApiTags.Day, id},
                {type: ApiTags.Date, id: "LIST"},
                {type: ApiTags.Template, id: "LIST"},
            ]
        })
    }),
})

export const {
    useGetDaysQuery,
    useLazyGetDaysQuery,
    useGetDayQuery,
    useLazyGetDayQuery,
    useGetCurrentDayQuery,
    useLazyGetCurrentDayQuery,
    useUpdateDayMutation,
} = dayApi