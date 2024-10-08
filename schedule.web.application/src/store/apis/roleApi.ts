import {baseApi} from "./baseApi.ts";
import {IRole} from "../../features/models";
import {ApiTags} from "./apiTags.ts";
import {IPagedList} from "../../features/models";
import {IPaginationQuery} from "../../features/queries";

export const roleApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getRoles: builder.query<IPagedList<IRole>, IPaginationQuery | void>({
            query: () => ({
                url: `${ApiTags.Role}`,
                method: "GET",
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Role, id} as const)),
                { type: ApiTags.Role, id: 'LIST' }
            ]
        }),
    }),
})

export const {
    useGetRolesQuery,
    useLazyGetRolesQuery,
} = roleApi