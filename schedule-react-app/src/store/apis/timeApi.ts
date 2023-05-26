import {ApiTags, baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ITime} from "../../features/models/ITime.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const timeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimes: builder.query<IPagedList<ITime>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Time}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, _, arg) => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Time, id} as const)),
                {
                    type: ApiTags.Time,
                    id: 'LIST',
                    page: result?.pageNumber,
                    search: arg?.search
                }
            ]
        }),
        getTime: builder.query<ITime, number>({
            query: id => ({
                url: `${ApiTags.Time}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.Time, id}
            ]
        }),
        restoreTime: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Time}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Time},
                {type: ApiTags.Lesson},
            ]
        }),
        createTime: builder.mutation<number, ITime>({
            query: time => ({
                url: ApiTags.Time,
                method: HttpMethod.POST,
                body: {
                    start: time.start,
                    end: time.end,
                    lessonNumber: time.lessonNumber,
                    typeId: time.type.id,
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Time},
            ]
        }),
        updateTime: builder.mutation<number, ITime>({
            query: time => ({
                url: ApiTags.Time,
                method: HttpMethod.PUT,
                body: {
                    id: time.id,
                    start: time.start,
                    end: time.end,
                    lessonNumber: time.lessonNumber,
                    typeId: time.type.id,
                    isDeleted: time.isDeleted,
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Time},
                {type: ApiTags.Lesson},
            ]
        }),
        deleteTime: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Time}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                {type: ApiTags.Time},
                {type: ApiTags.Lesson},
            ]
        }),
    })
})

export const {
    useGetTimesQuery,
    useLazyGetTimesQuery,
    useGetTimeQuery,
    useLazyGetTimeQuery,
    useCreateTimeMutation,
    useRestoreTimeMutation,
    useUpdateTimeMutation,
    useDeleteTimeMutation,
} = timeApi