import {ApiTags, baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IClassroom} from "../../features/models/IClassroom.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const classroomApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getClassrooms: builder.query<IPagedList<IClassroom>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Classroom}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Classroom, id} as const)),
                {type: ApiTags.Classroom, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getClassroom: builder.query<IClassroom, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Classroom, id}]
        }),
        restoreClassroom: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Classroom, id},
                {type: ApiTags.Lesson}
            ]
        }),
        createClassroom: builder.mutation<number, IClassroom>({
            query: classroom => ({
                url: ApiTags.Classroom,
                method: HttpMethod.POST,
                body: {
                    cabinet: classroom.cabinet,
                    typeIds: classroom.types.map(t => t.id)
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Classroom, id},
            ]
        }),
        updateClassroom: builder.mutation<number, IClassroom>({
            query: classroom => ({
                url: ApiTags.Classroom,
                method: HttpMethod.PUT,
                body: {
                    id: classroom.id,
                    cabinet: classroom.cabinet,
                    typeIds: classroom.types.map(t => t.id)
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Classroom, id},
                {type: ApiTags.Lesson},
            ]
        }),
        deleteClassroom: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: id => [
                {type: ApiTags.Classroom, id},
                {type: ApiTags.Lesson}
            ]
        })
    })
})

export const {
    useGetClassroomsQuery,
    useLazyGetClassroomsQuery,
    useGetClassroomQuery,
    useLazyGetClassroomQuery,
    useCreateClassroomMutation,
    useRestoreClassroomMutation,
    useUpdateClassroomMutation,
    useDeleteClassroomMutation
} = classroomApi