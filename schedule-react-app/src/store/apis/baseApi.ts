import {createApi} from "@reduxjs/toolkit/query/react";
import {fetchQueryWithReauth} from "../fetchBaseQueryWithReauth.ts";
import {ApiTags} from "./apiTags.ts";

export const baseApi = createApi({
    reducerPath: 'BaseApi',
    baseQuery: fetchQueryWithReauth,
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: () => ({}),
})