import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IClassroom} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const classroomApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getClassrooms: builder.query<IPagedList<IClassroom>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Classroom}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Classroom, id} as const)),
                {
                    type: ApiTags.Classroom,
                    id: 'LIST',
                    page: result?.pageNumber
                }
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
            invalidatesTags: () => [
                {type: ApiTags.Classroom},
                {type: ApiTags.Lesson}
            ]
        }),
        createClassroom: builder.mutation<number, IClassroom>({
            query: classroom => ({
                url: ApiTags.Classroom,
                method: HttpMethod.POST,
                body: {
                    cabinet: classroom.cabinet
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Classroom},
            ]
        }),
        updateClassroom: builder.mutation<number, IClassroom>({
            query: classroom => ({
                url: ApiTags.Classroom,
                method: HttpMethod.PUT,
                body: {
                    id: classroom.id,
                    cabinet: classroom.cabinet,
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Classroom},
                {type: ApiTags.Lesson},
            ]
        }),
        deleteClassroom: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: () => [
                {type: ApiTags.Classroom},
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