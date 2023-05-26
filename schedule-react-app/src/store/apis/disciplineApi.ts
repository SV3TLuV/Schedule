import {ApiTags, baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {IDiscipline} from "../../features/models/IDiscipline.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const disciplineApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplines: builder.query<IPagedList<IDiscipline>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Discipline}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, _, arg) => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Discipline, id} as const)),
                {
                    type: ApiTags.Discipline,
                    id: 'LIST',
                    page: result?.pageNumber,
                    search: arg?.search
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
                    name: discipline.name,
                    code: discipline.code,
                    totalHours: discipline.totalHours,
                    specialityId: discipline.speciality.id,
                    disciplineTypeId: discipline.type.id,
                    termId: discipline.term.id,
                    isDeleted: discipline.isDeleted,
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
                    name: discipline.name,
                    code: discipline.code,
                    totalHours: discipline.totalHours,
                    specialityId: discipline.speciality.id,
                    disciplineTypeId: discipline.type.id,
                    termId: discipline.term.id,
                    isDeleted: discipline.isDeleted,
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
        })
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