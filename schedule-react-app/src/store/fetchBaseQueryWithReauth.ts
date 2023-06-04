import {BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError} from "@reduxjs/toolkit/query/react";
import {Mutex} from "async-mutex";
import {AppState} from "./store.ts";
import {userApi} from "./apis/userApi.ts";
import {IRefreshCommand} from "../features/commands/IRefreshCommand.ts";
import {ILogoutCommand} from "../features/commands/ILogoutCommand.ts";

const mutex = new Mutex()

export const baseQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:7239/api/',
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json",
    },
    mode: "cors",
    prepareHeaders: (headers, { getState }) => {
        const accessToken = (getState() as AppState).auth.accessToken;
        if (accessToken) {
            headers.set("Authorization", `Bearer ${accessToken}`)
        }
        return headers
    }
})

export const fetchQueryWithReauth: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    await mutex.waitForUnlock()
    let result = await baseQuery(args, api, extraOptions)
    if (result.error && result.error.status === 401) {
        if (!mutex.isLocked()) {
            const release = await mutex.acquire()
            const authState = (api.getState() as AppState).auth;

            try {
                const user = authState.user

                if (user) {
                    await api.dispatch(userApi.endpoints.refresh.initiate({
                        accessToken: authState.accessToken,
                        refreshToken: authState.refreshToken
                    } as IRefreshCommand))
                }

                result = await baseQuery(args, api, extraOptions)
            }
            finally {
                if (result.error) {
                    try {
                        await api.dispatch(userApi.endpoints.logout.initiate({
                            accessToken: authState.accessToken,
                            refreshToken: authState.refreshToken,
                        } as ILogoutCommand))
                    } catch (e) {
                        console.log(e)
                    }
                }

                await release()
            }
        } else {
            await mutex.waitForUnlock()
            result = await baseQuery(args, api, extraOptions)
        }
    }
    return result
}