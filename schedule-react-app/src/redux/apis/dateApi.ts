import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {IPaginatedQueryWithFilters} from "../../features/queries/IPaginatedQueryWithFilters.ts";
import {IDate} from "../../features/models/IDate.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";

export const dateApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDates: builder.query<IPagedList<IDate>, IPaginatedQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Date}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Date, id} as const)),
                {type: ApiTags.Date, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getDate: builder.query<IDate, number>({
            query: id => ({
                url: `${ApiTags.Date}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Date, id}]
        }),
        getCurrentDate: builder.query<IDate, void>({
            query: () => ({
                url: `${ApiTags.Date}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: (result) => [
                {type: ApiTags.Date, id: 'CURRENT'},
                {type: ApiTags.Date, id: result?.id},
            ]
        }),
    }),
})

export const {
    useGetDatesQuery,
    useLazyGetDatesQuery,
    useGetDateQuery,
    useLazyGetDateQuery,
    useGetCurrentDateQuery,
    useLazyGetCurrentDateQuery,
} = dateApi