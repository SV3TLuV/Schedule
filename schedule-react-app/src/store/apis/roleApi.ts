import {baseApi} from "./baseApi.ts";
import {IRole} from "../../features/models/IRole.ts";
import {ApiTags} from "./apiTags.ts";

export const roleApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getRoles: builder.query<IRole[], void>({
            query: () => ({
                url: `${ApiTags.Roles}`,
                method: "GET",
            }),
            providesTags: result => [
                ...(result ?? []).map(({id}) => ({type: ApiTags.Roles, id} as const)),
                { type: ApiTags.Roles, id: 'LIST' }
            ]
        }),
    }),
})

export const {
    useGetRolesQuery,
    useLazyGetRolesQuery,
} = roleApi