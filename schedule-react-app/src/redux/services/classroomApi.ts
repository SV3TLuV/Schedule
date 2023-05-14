import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IClassroom} from "../../features/models/IClassroom.ts";
import {IPaginatedQueryWithFilter} from "../../features/queries/IPaginatedQueryWithFilter.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";

export const classroomApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getClassrooms: builder.query<IPagedList<IClassroom>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Classroom}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Classroom, id} as const)),
                {type: ApiTags.Classroom, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getClassroom: builder.query<IClassroom, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (result, error, id) => [{type: ApiTags.Classroom, id}]
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
                {type: ApiTags.Lesson, id: "LIST"},
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
                {type: ApiTags.Lesson, id: "LIST"},
            ]
        }),
        deleteClassroom: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Classroom}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: id => [
                {type: ApiTags.Classroom, id},
                {type: ApiTags.Lesson, id: "LIST"}
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
    useUpdateClassroomMutation,
    useDeleteClassroomMutation
} = classroomApi