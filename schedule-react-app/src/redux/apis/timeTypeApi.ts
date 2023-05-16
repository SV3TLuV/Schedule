import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ITimeType} from "../../features/models/ITimeType.ts";
import {IPaginatedQueryWithFilters} from "../../features/queries/IPaginatedQueryWithFilters.ts";

export const timeTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimeTypes: builder.query<IPagedList<ITimeType>, IPaginatedQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.TimeType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.TimeType, id} as const)),
                {type: ApiTags.TimeType, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getTimeType: builder.query<ITimeType, number>({
            query: id => ({
                url: `${ApiTags.TimeType}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.TimeType, id}
            ]
        }),
        createTimeType: builder.mutation<number, ITimeType>({
            query: timeType => ({
                url: ApiTags.TimeType,
                method: HttpMethod.POST,
                body: {
                    name: timeType.name,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.TimeType, id},
                {type: ApiTags.Time, id: 'LIST'},
            ]
        }),
        updateTimeType: builder.mutation<number, ITimeType>({
            query: timeType => ({
                url: ApiTags.TimeType,
                method: HttpMethod.PUT,
                body: {
                    id: timeType.id,
                    name: timeType.name,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.TimeType, id},
                {type: ApiTags.Time, id: 'LIST'},
            ]
        }),
        deleteTimeType: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.TimeType}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.TimeType, id},
                {type: ApiTags.Time, id: 'LIST'},
            ]
        }),
    }),
})

export const {
    useGetTimeTypesQuery,
    useLazyGetTimeTypesQuery,
    useGetTimeTypeQuery,
    useLazyGetTimeTypeQuery,
    useCreateTimeTypeMutation,
    useUpdateTimeTypeMutation,
    useDeleteTimeTypeMutation,
} = timeTypeApi