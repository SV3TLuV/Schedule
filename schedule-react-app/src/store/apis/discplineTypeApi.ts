import {baseApi} from "./baseApi.ts";
import {IDisciplineType} from "../../features/models";
import {IPagedList} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {HttpMethod} from "../../common/enums";
import {buildUrlArguments} from "../../utils/buildUrlArguments";
import {ApiTags} from "./apiTags.ts";

export const disciplineTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplineTypes: builder.query<IPagedList<IDisciplineType>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.DisciplineType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.DisciplineType, id} as const)),
                {
                    type: ApiTags.DisciplineType,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ],
        }),
        getDisciplineType: builder.query<IDisciplineType, number>({
            query: id => ({
                url: `${ApiTags.DisciplineType}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [
                {type: ApiTags.DisciplineType, id}
            ]
        })
    })
})

export const {
    useGetDisciplineTypesQuery,
    useLazyGetDisciplineTypesQuery,
    useGetDisciplineTypeQuery,
    useLazyGetDisciplineTypeQuery
} = disciplineTypeApi