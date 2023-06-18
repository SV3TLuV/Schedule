import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {IPagedList} from "../../features/models";
import {IWeekType} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const weekTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getWeekTypes: builder.query<IPagedList<IWeekType>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.WeekType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.WeekType, id} as const)),
                {
                    type: ApiTags.WeekType,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
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
        getCurrentWeekType: builder.query<IWeekType, void>({
            query: () => ({
                url: `${ApiTags.WeekType}/current`,
                method: HttpMethod.GET,
            }),
            providesTags: () => [
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