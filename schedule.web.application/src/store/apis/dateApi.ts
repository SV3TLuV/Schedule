import {baseApi} from "./baseApi.ts";
import {IDate} from "../../features/models";
import {IPagedList} from "../../features/models";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";
import {IGetDatesQuery} from "../../features/queries/IGetDatesQuery.ts";

export const dateApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDates: builder.query<IPagedList<IDate>, IGetDatesQuery | void>({
            query: query => ({
                url: `${ApiTags.Date}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Date, id} as const)),
                {
                    type: ApiTags.Date,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
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
        })
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