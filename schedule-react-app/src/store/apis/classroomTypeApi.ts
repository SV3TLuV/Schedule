import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IClassroomType} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const classroomTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getClassroomTypes: builder.query<IPagedList<IClassroomType>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.ClassroomType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.ClassroomType, id} as const)),
                {
                    type: ApiTags.ClassroomType,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getClassroomType: builder.query<IClassroomType, number>({
            query: id => ({
                url: `${ApiTags.ClassroomType}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (_, __, id) => [{type: ApiTags.ClassroomType, id}]
        }),
        restoreClassroomType: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.ClassroomType}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.ClassroomType},
                {type: ApiTags.Classroom},
                {type: ApiTags.Lesson},
            ]
        }),
        createClassroomType: builder.mutation<number, IClassroomType>({
            query: classroomType => ({
                url: ApiTags.ClassroomType,
                method: HttpMethod.POST,
                body: {
                    name: classroomType.name,
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.ClassroomType},
            ]
        }),
        updateClassroomType: builder.mutation<number, IClassroomType>({
            query: classroomType => ({
                url: ApiTags.ClassroomType,
                method: HttpMethod.PUT,
                body: {
                    id: classroomType.id,
                    name: classroomType.name,
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.ClassroomType},
                {type: ApiTags.Classroom},
                {type: ApiTags.Lesson},
            ]
        }),
        deleteClassroomType: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.ClassroomType}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: () => [
                {type: ApiTags.ClassroomType},
                {type: ApiTags.Classroom},
                {type: ApiTags.Lesson},
            ]
        })
    }),
})

export const {
    useGetClassroomTypesQuery,
    useLazyGetClassroomTypesQuery,
    useGetClassroomTypeQuery,
    useLazyGetClassroomTypeQuery,
    useCreateClassroomTypeMutation,
    useRestoreClassroomTypeMutation,
    useUpdateClassroomTypeMutation,
    useDeleteClassroomTypeMutation
} = classroomTypeApi