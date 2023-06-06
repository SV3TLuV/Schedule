import {BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError} from "@reduxjs/toolkit/query/react";
import {Mutex} from "async-mutex";
import {AppState} from "./store.ts";
import {IRefreshCommand} from "../features/commands";
import {message} from "antd";
import {userApi} from "./apis";
import {API_URL} from "../configuration.ts";

const mutex = new Mutex()

export const baseQuery = fetchBaseQuery({
    baseUrl: `${API_URL}/api`,
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

async function showError(error: FetchBaseQueryError | undefined) {
    if (!error) {
        return;
    }

    let msg = 'Неизвестная ошибка!'

    const { status, data } = error

    if (status && data && (data as any).error) {
        msg = `[${status}] ${(data as any)?.error}`
    }

    message.error(msg);
}

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
                await showError(result.error)
                await release()
            }
        } else {
            await mutex.waitForUnlock()
            result = await baseQuery(args, api, extraOptions)
        }
    }
    await showError(result.error)
    return result
}