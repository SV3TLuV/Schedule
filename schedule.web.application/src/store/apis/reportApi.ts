import {ApiTags} from "./apiTags.ts";
import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {baseQuery} from "../fetchBaseQueryWithReauth.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/dist/query/react";
import {IDownloadTimetableReport} from "../../features/commands";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const reportApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        downloadTimetableReport: builder.mutation<void, IDownloadTimetableReport | void>({
            queryFn: async (command, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTags.Report}/timetable?${buildUrlArguments(command ?? {})}`,
                    method: HttpMethod.GET,
                    responseHandler: response => response.blob()
                }, api, extraOptions)

                const headers = response!.meta?.response?.headers;

                if (headers) {
                    const header = headers.get('content-disposition') ?? '';
                    const regex = /filename[^*]?=\s*["']?(.*?)["'];?/;
                    const matches = header.match(regex);

                    if (matches && matches.length > 1) {
                        const fileName = matches[1];

                        const blob = response.data as Blob
                        const downloadUrl = window.URL.createObjectURL(blob)
                        const link = document.createElement('a')
                        link.href = downloadUrl
                        link.setAttribute('download', fileName)
                        document.body.appendChild(link)
                        link.click()
                        link.remove()
                        window.URL.revokeObjectURL(downloadUrl)
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