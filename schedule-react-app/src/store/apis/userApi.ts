import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IUser} from "../../features/models";
import {IPaginationQuery} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";
import {IAuthorizationResult} from "../../features/models";
import {ILoginCommand, ILogoutCommand, IRefreshCommand} from "../../features/commands";
import {login, logout} from "../slices";
import {ApiTags} from "./apiTags.ts";
import {baseQuery} from "../fetchBaseQueryWithReauth.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/query/react";
import {AppState} from "../store.ts";

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
        logout: builder.mutation<void, ILogoutCommand>({
            query: command => ({
                url: `${ApiTags.Users}/logout`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled}) {
                try {
                    await queryFulfilled
                    await dispatch(logout())
                } catch (e) {
                    console.log(e)
                }
            },
        }),
        refresh: builder.mutation<IAuthorizationResult, IRefreshCommand>({
            queryFn: async (command, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTags.Users}/refresh`,
                    method: HttpMethod.POST,
                    body: command,
                }, api, extraOptions)

                if (response.data) {
                    const result = response.data as IAuthorizationResult
                    await api.dispatch(login(result))
                    return { data: result }
                }
                const authState = (api.getState() as AppState).auth;
                await api.dispatch(userApi.endpoints.logout.initiate({
                    accessToken: authState.accessToken,
                    refreshToken: authState.refreshToken
                } as ILogoutCommand))
                return { error: response.error as FetchBaseQueryError }
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
    useLogoutMutation,
    useRefreshMutation,
    useCreateUserMutation,
    useUpdateUserMutation,
    useDeleteUserMutation,
} = userApi;

