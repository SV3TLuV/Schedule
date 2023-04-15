import {IPagedList} from "../features/models/IPagedList";
import {IGroup} from "../features/models/IGroup";
import {HttpMethod} from "../common/enums/HttpMethod";
import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {IPaginatedQueryWithFilter} from "../features/queries/IPaginatedQueryWithFilter";
import {IPaginatedQuery} from "../features/queries/IPaginatedQuery";


export const groupApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getGroups: builder.query<IPagedList<IGroup>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Group}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Group, id} as const)),
                {type: ApiTags.Group, id: "LIST", page: result?.pageNumber}
            ]
        })
    }),
})

export const {
    useGetGroupsQuery,
    useLazyGetGroupsQuery
} = groupApi