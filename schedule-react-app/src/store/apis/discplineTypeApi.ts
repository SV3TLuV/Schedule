import {ApiTags, baseApi} from "./baseApi";
import {IDisciplineType} from "../../features/models/IDisciplineType";
import {IPagedList} from "../../features/models/IPagedList";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters";
import {HttpMethod} from "../../common/enums/HttpMethod";
import {buildUrlArguments} from "../../utils/buildUrlArguments";

export const disciplineTypeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getDisciplineTypes: builder.query<IPagedList<IDisciplineType>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.DisciplineType}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.DisciplineType, id} as const)),
                {type: ApiTags.DisciplineType, id: 'LIST', page: result?.pageNumber}
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