import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {IPagedList} from "@/features/models/IPagedList";
import {ITime} from "@/features/models/ITime";
import {IPaginatedQueryWithFilter} from "@/features/queries/IPaginatedQueryWithFilter";
import {HttpMethod} from "@/common/enums/HttpMethod";

export const timeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTimes: builder.query<IPagedList<ITime>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Time}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Time, id} as const)),
                {type: ApiTags.Time, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getTime: builder.query<ITime, number>({
            query: id => ({
                url: `${ApiTags.Time}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Time, id}
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
            invalidatesTags: id => [
                {type: ApiTags.Time, id},
                {type: ApiTags.Lesson, id: "LIST"},
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
        }),
        deleteTime: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Time}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Time, id},
                {type: ApiTags.Lesson, id: "LIST"},
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
    useUpdateTimeMutation,
    useDeleteTimeMutation,
} = timeApi

export const {
    getTimes,
    getTime,
    createTime,
    updateTime,
    deleteTime,
} = timeApi.endpoints