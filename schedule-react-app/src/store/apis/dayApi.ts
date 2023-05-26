import {ApiTags, baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IDay} from "../../features/models/IDay.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const dayApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDays: builder.query<IPagedList<IDay>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Day}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, _, arg) => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Day, id} as const)),
                {
                    type: ApiTags.Day,
                    id: 'LIST',
                    page: result?.pageNumber,
                    search: arg?.search
                }
            ]
        }),
        getDay: builder.query<IDay, number>({
            query: id => ({
                url: `${ApiTags.Day}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Day, id}]
        }),
        getCurrentDay: builder.query<IDay, void>({
            query: () => ({
                url: `${ApiTags.Day}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                {type: ApiTags.Day, id: 'CURRENT'},
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
            invalidatesTags: () => [
                {type: ApiTags.Day},
                {type: ApiTags.Date},
                {type: ApiTags.Template},
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