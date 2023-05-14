import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {IPagedList} from "@/features/models/IPagedList";
import {ITeacher} from "@/features/models/ITeacher";
import {IPaginatedQueryWithFilter} from "@/features/queries/IPaginatedQueryWithFilter";
import {HttpMethod} from "@/common/enums/HttpMethod";

export const teacherApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTeachers: builder.query<IPagedList<ITeacher>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Teacher}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Teacher, id} as const)),
                {type: ApiTags.Teacher, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getTeacher: builder.query<ITeacher, number>({
            query: id => ({
                url: `${ApiTags.Teacher}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Teacher, id}
            ]
        }),
        createTeacher: builder.mutation<number, ITeacher>({
            query: teacher => ({
                url: ApiTags.Teacher,
                method: HttpMethod.POST,
                body: {
                    surname: teacher.surname,
                    name: teacher.name,
                    middleName: teacher.middleName,
                    email: teacher.email,
                    groupIds: teacher.groups.map(g => g.id),
                    disciplineIds: teacher.disciplines.map(d => d.id),
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Teacher, id},
                {type: ApiTags.Lesson, id: "LIST"}
            ]
        }),
        updateTeacher: builder.mutation<number, ITeacher>({
            query: teacher => ({
                url: ApiTags.Teacher,
                method: HttpMethod.PUT,
                body: {
                    id: teacher.id,
                    surname: teacher.surname,
                    name: teacher.name,
                    middleName: teacher.middleName,
                    email: teacher.email,
                    groupIds: teacher.groups.map(g => g.id),
                    disciplineIds: teacher.disciplines.map(d => d.id),
                    isDeleted: teacher.isDeleted,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Teacher, id},
                {type: ApiTags.Lesson, id: "LIST"}
            ]
        }),
        deleteTeacher: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Teacher}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Teacher, id},
                {type: ApiTags.Lesson, id: "LIST"}
            ]
        }),
    }),
})

export const  {
    useGetTeachersQuery,
    useLazyGetTeachersQuery,
    useGetTeacherQuery,
    useLazyGetTeacherQuery,
    useCreateTeacherMutation,
    useUpdateTeacherMutation,
    useDeleteTeacherMutation,
} = teacherApi

export const {
    getTeachers,
    getTeacher,
    createTeacher,
    updateTeacher,
    deleteTeacher,
} = teacherApi.endpoints