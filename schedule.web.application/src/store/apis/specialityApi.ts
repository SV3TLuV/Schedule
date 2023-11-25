import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {IPagedList} from "../../features/models";
import {ISpeciality} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";
import {baseQuery} from "../fetchBaseQueryWithReauth.ts";
import {saveFile} from "../../utils/saveFile.ts";
import {FetchBaseQueryError} from "@reduxjs/toolkit/dist/query/react";
import {getFileNameFromHeaders} from "../../utils/getFileNameFromHeaders.ts";

export const specialityApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getSpecialities: builder.query<IPagedList<ISpeciality>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Speciality}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Speciality, id} as const)),
                {
                    type: ApiTags.Speciality,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getSpeciality: builder.query<ISpeciality, number>({
            query: id => ({
                url: `${ApiTags.Speciality}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Speciality, id}]
        }),
        restoreSpeciality: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Speciality}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Speciality},
                {type: ApiTags.Group},
                {type: ApiTags.Discipline},
            ]
        }),
        createSpeciality: builder.mutation<number, ISpeciality>({
            query: speciality => ({
                url: ApiTags.Speciality,
                method: HttpMethod.POST,
                body: {
                    code: speciality.code,
                    name: speciality.name,
                    maxTermId: speciality.maxTermId
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Speciality},
            ]
        }),
        updateSpeciality: builder.mutation<number, ISpeciality>({
            query: speciality => ({
                url: ApiTags.Speciality,
                method: HttpMethod.PUT,
                body: {
                    id: speciality.id,
                    code: speciality.code,
                    name: speciality.name,
                    maxTermId: speciality.maxTermId
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Speciality},
                {type: ApiTags.Group},
                {type: ApiTags.Discipline},
            ]
        }),
        deleteSpeciality: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Speciality}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                {type: ApiTags.Speciality},
                {type: ApiTags.Group},
                {type: ApiTags.Discipline},
            ]
        }),
        exportSpeciality: builder.mutation<void, void>({
            queryFn: async (_, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTags.Speciality}/export`,
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
        importSpeciality: builder.mutation<void, Blob>({
            queryFn: async (blob, api, extraOptions) => {
                const response = await baseQuery({
                    url: `${ApiTags.Speciality}/import`,
                    method: HttpMethod.POST,
                    body: blob
                }, api, extraOptions)

                return { error: response.error as FetchBaseQueryError }
            },
        }),
    }),
})

export const {
    useGetSpecialitiesQuery,
    useLazyGetSpecialitiesQuery,
    useGetSpecialityQuery,
    useLazyGetSpecialityQuery,
    useCreateSpecialityMutation,
    useRestoreSpecialityMutation,
    useUpdateSpecialityMutation,
    useDeleteSpecialityMutation,
    useExportSpecialityMutation,
    useImportSpecialityMutation,
} = specialityApi