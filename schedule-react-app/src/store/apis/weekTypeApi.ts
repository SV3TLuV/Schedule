import {ApiTags, baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IWeekType} from "../../features/models/IWeekType.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const weekTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getWeekTypes: builder.query<IPagedList<IWeekType>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.WeekType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.WeekType, id} as const)),
                {type: ApiTags.WeekType, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getWeekType: builder.query<IWeekType, number>({
            query: id => ({
                url: `${ApiTags.WeekType}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.WeekType, id}
            ]
        }),
        getCurrentWeekType: builder.query<IWeekType, number>({
            query: () => ({
                url: `${ApiTags.WeekType}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.WeekType, id},
                {type: ApiTags.WeekType, id: 'CURRENT'},
            ]
        }),
    }),
})

export const {
    useGetWeekTypesQuery,
    useLazyGetWeekTypesQuery,
    useGetWeekTypeQuery,
    useLazyGetWeekTypeQuery,
    useGetCurrentWeekTypeQuery,
    useLazyGetCurrentWeekTypeQuery,
} = weekTypeApi