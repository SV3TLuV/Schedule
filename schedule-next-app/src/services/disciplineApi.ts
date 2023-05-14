import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "@/common/enums/HttpMethod";
import {IPaginatedQueryWithFilter} from "@/features/queries/IPaginatedQueryWithFilter";
import {IDiscipline} from "@/features/models/IDiscipline";
import {IPagedList} from "@/features/models/IPagedList";

export const disciplineApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplines: builder.query<IPagedList<IDiscipline>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Discipline}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Discipline, id} as const)),
                {type: ApiTags.Discipline, id: "LIST", page: result?.pageNumber}
            ],
        }),
        getDiscipline: builder.query<IDiscipline, number>({
            query: id => ({
                url: `${ApiTags.Discipline}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Discipline, id}
            ],
        }),
        createDiscipline: builder.mutation<number, IDiscipline>({
            query: discipline => ({
                url: ApiTags.Discipline,
                method: HttpMethod.POST,
                body: {
                    name: discipline.name,
                    code: discipline.code,
                    totalHours: discipline.totalHours,
                    specialityCodeId: discipline.speciality.id,
                    termId: discipline.term.value,
                    isDeleted: discipline.isDeleted,
                }
            }),
            invalidatesTags: id => [
                { type: ApiTags.Discipline, id },
                { type: ApiTags.Lesson, id: "LIST" }
            ],
        }),
        updateDiscipline: builder.mutation<number, IDiscipline>({
            query: discipline => ({
                url: ApiTags.Discipline,
                method: HttpMethod.PUT,
                body: {
                    id: discipline.id,
                    name: discipline.name,
                    code: discipline.code,
                    totalHours: discipline.totalHours,
                    specialityCodeId: discipline.speciality.id,
                    termId: discipline.term.value,
                    isDeleted: discipline.isDeleted,
                }
            }),
            invalidatesTags: id => [
                { type: ApiTags.Discipline, id },
                { type: ApiTags.Lesson, id: "LIST" }
            ],
        }),
        deleteDiscipline: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Discipline}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                { type: ApiTags.Discipline, id },
                { type: ApiTags.Lesson, id: "LIST" }
            ],
        })
    }),
})

export const {
    useGetDisciplinesQuery,
    useLazyGetDisciplinesQuery,
    useGetDisciplineQuery,
    useLazyGetDisciplineQuery,
    useCreateDisciplineMutation,
    useUpdateDisciplineMutation,
    useDeleteDisciplineMutation,
} = disciplineApi

export const {
    getDisciplines,
    getDiscipline,
    createDiscipline,
    updateDiscipline,
    deleteDiscipline,
} = disciplineApi.endpoints