import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {ITeacher} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const teacherApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTeachers: builder.query<IPagedList<ITeacher>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Teacher}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Teacher, id} as const)),
                {
                    type: ApiTags.Teacher,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getTeacher: builder.query<ITeacher, number>({
            query: id => ({
                url: `${ApiTags.Teacher}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.Teacher, id}
            ]
        }),
        restoreTeacher: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Teacher}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Teacher},
                {type: ApiTags.Lesson},
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
            invalidatesTags: () => [
                {type: ApiTags.Teacher},
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
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Teacher},
                {type: ApiTags.Lesson},
            ]
        }),
        deleteTeacher: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Teacher}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                {type: ApiTags.Teacher},
                {type: ApiTags.Lesson},
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
    useRestoreTeacherMutation,
    useUpdateTeacherMutation,
    useDeleteTeacherMutation,
} = teacherApi