import {createApi} from "@reduxjs/toolkit/dist/query/react";
import {baseQuery} from "../fetchBaseQueryWithReauth.ts";
import {ApiTags} from "./baseApi.ts";
import {IAuthorizationResult} from "../../features/models/IAuthorizationResult.ts";
import {ILoginCommand} from "../../features/commands/ILoginCommand.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {login, logout} from "../slices/authSlice.ts";
import {ILogoutCommand} from "../../features/commands/ILogoutCommand.ts";
import {IRefreshCommand} from "../../features/commands/IRefreshCommand.ts";
import {AppState} from "../store.ts";

export const accountApi = createApi({
    reducerPath: 'BaseApi',
    baseQuery: baseQuery,
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({
        login: builder.mutation<IAuthorizationResult, ILoginCommand>({
            query: command => ({
                url: `${ApiTags.Users}/login`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled}) {
                try {
                    const {data} = await queryFulfilled
                    dispatch(login(data))
                } catch (e) {
                    console.log(e)
                }
            }
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
                } catch (e) {
                    console.log(e)
                } finally {
                    await dispatch(logout())
                }
            },
            transformErrorResponse: (error) => {
                throw error
            }
        }),
        refresh: builder.mutation<IAuthorizationResult, IRefreshCommand>({
            query: command => ({
                url: `${ApiTags.Users}/refresh`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled, getState}) {
                try {
                    console.log('try refresh start')
                    const {data} = await queryFulfilled
                    await dispatch(login(data))
                } catch (e) {
                    const authState = (getState() as AppState).auth;
                    await dispatch(accountApi.endpoints.logout.initiate({
                        accessToken: authState.accessToken,
                        refreshToken: authState.refreshToken
                    } as ILogoutCommand))
                    console.log(e)
                }
            },
            transformErrorResponse: (error) => {
                throw error
            }
        }),
    })
})

export const {
    useLoginMutation,
    useLogoutMutation,
    useRefreshMutation,
} = accountApi