import {ApiTags} from "./apiTags.ts";
import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {baseQuery} from "../fetchBaseQueryWithReauth.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/dist/query/react";
import {IDownloadTimetableReport} from "../../features/commands";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {saveFile} from "../../utils/saveFile.ts";
import {getFileNameFromHeaders} from "../../utils/getFileNameFromHeaders.ts";

export const reportApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        downloadTimetableReport: builder.mutation<void, IDownloadTimetableReport | void>({
            queryFn: async (command, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTags.Report}/timetable?${buildUrlArguments(command ?? {})}`,
                    method: HttpMethod.GET,
                    responseHandler: response => response.blob()
                }, api, extraOptions)

                const headers = response!.meta?.response?.headers

                if (headers) {
                    const fileName = getFileNameFromHeaders(headers)

                    if (fileName) {
                        const blob = response.data as Blob
                        saveFile(fileName, blob)
                    }
                }

                return { error: response.error as FetchBaseQueryError }
            },
        }),
    })
})

export const {
    useDownloadTimetableReportMutation
} = reportApi