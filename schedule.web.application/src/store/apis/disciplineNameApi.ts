import {baseApi} from "./baseApi.ts";
import {IDisciplineName, IPagedList} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {ApiTags} from "./apiTags.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";

export const disciplineNameApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplineNames: builder.query<IPagedList<IDisciplineName>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.DisciplineName}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.DisciplineName, id} as const)),
                {
                    type: ApiTags.DisciplineName,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ],
        }),
        getDisciplineName: builder.query<IDisciplineName, number>({
            query: id => ({
                url: `${ApiTags.DisciplineName}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.DisciplineName, id}
            ],
        }),
        restoreDisciplineName: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.DisciplineName}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineName },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        createDisciplineName: builder.mutation<number, IDisciplineName>({
            query: discipline => ({
                url: ApiTags.DisciplineName,
                method: HttpMethod.POST,
                body: {
                    name: discipline.name,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineName },
                { type: ApiTags.Discipline },
            ]
        }),
        updateDisciplineName: builder.mutation<number, IDisciplineName>({
            query: discipline => ({
                url: ApiTags.DisciplineName,
                method: HttpMethod.PUT,
                body: {
                    id: discipline.id,
                    name: discipline.name,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineName },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        deleteDisciplineName: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.DisciplineName}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineName },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
    })
})

export const {
    useGetDisciplineNamesQuery,
    useLazyGetDisciplineNamesQuery,
    useGetDisciplineNameQuery,
    useLazyGetDisciplineNameQuery,
    useCreateDisciplineNameMutation,
    useRestoreDisciplineNameMutation,
    useUpdateDisciplineNameMutation,
    useDeleteDisciplineNameMutation,
} = disciplineNameApi