import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IUser} from "../../features/models";
import {IPaginationQuery} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";
import {IAuthorizationResult} from "../../features/models";
import {ILoginCommand} from "../../features/commands";
import {login} from "../slices";
import {ApiTags} from "./apiTags.ts";

export const userApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getUsers: builder.query<IPagedList<IUser>, IPaginationQuery | void>({
            query: query => ({
                url: `${ApiTags.Users}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Users, id} as const)),
                {
                    type: ApiTags.Users,
                    id: 'LIST',
                    page: result?.pageNumber
                }
            ]
        }),
        getUser: builder.query<IUser, number>({
            query: id => `${ApiTags.Users}/${id}`,
            providesTags: result => [{type: ApiTags.Users, id: result?.id}]
        }),
        login: builder.mutation<IAuthorizationResult, ILoginCommand>({
            query: command => ({
                url: `${ApiTags.Users}/login`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled}) {
                try {
                    const {data} = await queryFulfilled
                    await dispatch(login(data))
                } catch (e) {
                    console.log(e)
                }
            },
        }),
        createUser: builder.mutation<number, IUser>({
            query: user => ({
                url: ApiTags.Users,
                method: HttpMethod.POST,
                body: {
                    login: user.login,
                    password: user.password,
                    roleId: user.role.id
                },
            }),
            invalidatesTags: [{type: ApiTags.Users}]
        }),
        updateUser: builder.mutation<number, IUser>({
            query: user => ({
                url: ApiTags.Users,
                method: HttpMethod.PUT,
                body: {
                    id: user.id,
                    login: user.login,
                    password: user.password,
                    roleId: user.role.id
                },
            }),
            invalidatesTags: [{type: ApiTags.Users}]
        }),
        deleteUser: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Users}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: [{type: ApiTags.Users}]
        }),
    }),
})

export const {
    useGetUsersQuery,
    useLazyGetUsersQuery,
    useGetUserQuery,
    useLazyGetUserQuery,
    useLoginMutation,
    useCreateUserMutation,
    useUpdateUserMutation,
    useDeleteUserMutation,
} = userApi;

