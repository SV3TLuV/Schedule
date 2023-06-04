import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {ITerm} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments";
import {HttpMethod} from "../../common/enums";
import {ApiTags} from "./apiTags.ts";

export const termApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTerms: builder.query<IPagedList<ITerm>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Term}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Term, id} as const)),
                {
                    type: ApiTags.Term,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ],
        }),
        getTerm: builder.query<ITerm, number>({
            query: id => ({
                url: `${ApiTags.Term}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Term, id}]
        })
    })
})

export const {
    useGetTermsQuery,
    useLazyGetTermsQuery,
    useGetTermQuery,
    useLazyGetTermQuery
} = termApi