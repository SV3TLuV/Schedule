import {baseApi} from "./baseApi.ts";
import {IDisciplineCode, IPagedList} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {ApiTags} from "./apiTags.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";

export const disciplineCodeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplineCodes: builder.query<IPagedList<IDisciplineCode>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.DisciplineCode}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.DisciplineCode, id} as const)),
                {
                    type: ApiTags.DisciplineCode,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ],
        }),
        getDisciplineCode: builder.query<IDisciplineCode, number>({
            query: id => ({
                url: `${ApiTags.DisciplineCode}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.DisciplineCode, id}
            ],
        }),
        restoreDisciplineCode: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.DisciplineCode}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineCode },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        createDisciplineCode: builder.mutation<number, IDisciplineCode>({
            query: discipline => ({
                url: ApiTags.DisciplineCode,
                method: HttpMethod.POST,
                body: {
                    code: discipline.code,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineCode },
                { type: ApiTags.Discipline },
            ]
        }),
        updateDisciplineCode: builder.mutation<number, IDisciplineCode>({
            query: discipline => ({
                url: ApiTags.DisciplineCode,
                method: HttpMethod.PUT,
                body: {
                    id: discipline.id,
                    code: discipline.code,
                }
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineCode },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
        deleteDisciplineCode: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.DisciplineCode}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                { type: ApiTags.DisciplineCode },
                { type: ApiTags.Discipline },
                { type: ApiTags.Lesson }
            ]
        }),
    })
})

export const {
    useGetDisciplineCodesQuery,
    useLazyGetDisciplineCodesQuery,
    useGetDisciplineCodeQuery,
    useLazyGetDisciplineCodeQuery,
    useCreateDisciplineCodeMutation,
    useRestoreDisciplineCodeMutation,
    useUpdateDisciplineCodeMutation,
    useDeleteDisciplineCodeMutation,
} = disciplineCodeApi