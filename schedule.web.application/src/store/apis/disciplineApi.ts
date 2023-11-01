import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {IDiscipline} from "../../features/models";
import {IPagedList} from "../../features/models";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

export const disciplineApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplines: builder.query<IPagedList<IDiscipline>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Discipline}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Discipline, id} as const)),
                {
                    type: ApiTags.Discipline,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ],
        }),
        getDiscipline: builder.query<IDiscipline, number>({
            query: id => ({
                url: `${ApiTags.Discipline}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.Discipline, id}
            ],
        }),
        restoreDiscipline: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Discipline}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        createDiscipline: builder.mutation<number, IDiscipline>({
            query: discipline => ({
                url: ApiTags.Discipline,
                method: HttpMethod.POST,
                body: {
                    nameId: discipline.name.id,
                    codeId: discipline.code.id,
                    totalHours: discipline.totalHours,
                    specialityId: discipline.speciality.id,
                    disciplineTypeId: discipline.type.id,
                    termId: discipline.term.id,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.Discipline },
            ]
        }),
        updateDiscipline: builder.mutation<number, IDiscipline>({
            query: discipline => ({
                url: ApiTags.Discipline,
                method: HttpMethod.PUT,
                body: {
                    id: discipline.id,
                    nameId: discipline.name.id,
                    codeId: discipline.code.id,
                    totalHours: discipline.totalHours,
                    specialityId: discipline.speciality.id,
                    disciplineTypeId: discipline.type.id,
                    termId: discipline.term.id,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        deleteDiscipline: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Discipline}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
    }),
})

export const {
    useGetDisciplinesQuery,
    useLazyGetDisciplinesQuery,
    useGetDisciplineQuery,
    useLazyGetDisciplineQuery,
    useCreateDisciplineMutation,
    useRestoreDisciplineMutation,
    useUpdateDisciplineMutation,
    useDeleteDisciplineMutation,
} = disciplineApi