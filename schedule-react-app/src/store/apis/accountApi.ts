import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/dist/query/react";
import {IAuthorizationResult} from "../../features/models";
import {HttpMethod} from "../../common/enums";
import {login, logout} from "../slices";
import {ILogoutCommand} from "../../features/commands";
import {IRefreshCommand} from "../../features/commands";
import {AppState} from "../store.ts";
import {ApiTags} from "./apiTags.ts";

export const accountApi = createApi({
    reducerPath: 'BaseApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'https://localhost:7239/api/',
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
        },
        mode: "cors",
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({
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
                    await dispatch(logout())
                }
            },
            transformErrorResponse: (error) => {
                throw error
            },
            transformResponse: (response: void) => response
        }),
        refresh: builder.mutation<IAuthorizationResult, IRefreshCommand>({
            query: command => ({
                url: `${ApiTags.Users}/refresh`,
                method: HttpMethod.POST,
                body: command,
            }),
            async onQueryStarted(_, {dispatch, queryFulfilled, getState}) {
                try {
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
            },
            transformResponse: (response: IAuthorizationResult) => response
        }),
    })
})

export const {
    useLogoutMutation,
    useRefreshMutation,
} = accountApi