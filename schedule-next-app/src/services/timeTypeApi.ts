import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "@/common/enums/HttpMethod";
import {IPagedList} from "@/features/models/IPagedList";
import {ITimeType} from "@/features/models/ITimeType";
import {IPaginatedQueryWithFilter} from "@/features/queries/IPaginatedQueryWithFilter";

export const timeTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimeTypes: builder.query<IPagedList<ITimeType>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.TimeType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.TimeType, id} as const)),
                {type: ApiTags.TimeType, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getTimeType: builder.query<ITimeType, number>({
            query: id => ({
                url: `${ApiTags.TimeType}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
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
                {type: ApiTags.Time, id: "LIST"},
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
                {type: ApiTags.Time, id: "LIST"},
            ]
        }),
        deleteTimeType: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.TimeType}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.TimeType, id},
                {type: ApiTags.Time, id: "LIST"},
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

export const {
    getTimeTypes,
    getTimeType,
    createTimeType,
    updateTimeType,
    deleteTimeType,
} = timeTypeApi.endpoints