import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {IPagedList} from "../features/models/IPagedList";
import {IClassroomType} from "../features/models/IClassroomType";
import {IPaginatedQueryWithFilter} from "../features/queries/IPaginatedQueryWithFilter";
import {HttpMethod} from "../common/enums/HttpMethod";


export const classroomTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getClassroomTypes: builder.query<IPagedList<IClassroomType>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.ClassroomType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.ClassroomType, id} as const)),
                {type: ApiTags.ClassroomType, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getClassroomType: builder.query<IClassroomType, number>({
            query: id => ({
                url: `${ApiTags.ClassroomType}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (result, error, id) => [{type: ApiTags.ClassroomType, id}]
        }),
        createClassroomType: builder.mutation<number, IClassroomType>({
            query: classroomType => ({
                url: ApiTags.ClassroomType,
                method: HttpMethod.POST,
                body: {
                    name: classroomType.name,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.ClassroomType, id},
                {type: ApiTags.Classroom, id: "LIST"},
                {type: ApiTags.Lesson, id: "LIST"},
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
            invalidatesTags: id => [
                {type: ApiTags.ClassroomType, id},
                {type: ApiTags.Classroom, id: "LIST"},
                {type: ApiTags.Lesson, id: "LIST"},
            ]
        }),
        deleteClassroomType: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.ClassroomType}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: id => [
                {type: ApiTags.ClassroomType, id},
                {type: ApiTags.Classroom, id: "LIST"},
                {type: ApiTags.Lesson, id: "LIST"},
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
    useUpdateClassroomTypeMutation,
    useDeleteClassroomTypeMutation
} = classroomTypeApi