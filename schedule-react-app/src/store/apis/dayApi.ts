import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IDay} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const dayApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDays: builder.query<IPagedList<IDay>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Day}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Day, id} as const)),
                {
                    type: ApiTags.Day,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getDay: builder.query<IDay, number>({
            query: id => ({
                url: `${ApiTags.Day}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Day, id}],
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