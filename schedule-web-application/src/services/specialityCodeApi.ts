import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "../common/enums/HttpMethod";
import {IPagedList} from "../features/models/IPagedList";
import {ISpecialityCode} from "../features/models/ISpecialityCode";
import {IPaginatedQueryWithFilter} from "../features/queries/IPaginatedQueryWithFilter";

export const specialityCodeApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getSpecialityCodes: builder.query<IPagedList<ISpecialityCode>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.SpecialityCode}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.SpecialityCode, id} as const)),
                {type: ApiTags.SpecialityCode, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getSpecialityCode: builder.query<ISpecialityCode, number>({
            query: id => ({
                url: `${ApiTags.SpecialityCode}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.SpecialityCode, id}
            ]
        }),
        createSpecialityCode: builder.mutation<number, ISpecialityCode>({
            query: specialityCode => ({
                url: ApiTags.SpecialityCode,
                method: HttpMethod.POST,
                body: {
                    code: specialityCode.code,
                    name: specialityCode.name,
                    disciplineIds: specialityCode.disciplines.map(d => d.id),
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.SpecialityCode, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
            ]
        }),
        updateSpecialityCode: builder.mutation<number, ISpecialityCode>({
            query: specialityCode => ({
                url: ApiTags.SpecialityCode,
                method: HttpMethod.PUT,
                body: {
                    id: specialityCode.id,
                    code: specialityCode.code,
                    name: specialityCode.name,
                    disciplineIds: specialityCode.disciplines.map(d => d.id),
                    isDeleted: specialityCode.isDeleted,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.SpecialityCode, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
            ]
        }),
        deleteSpecialityCode: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.SpecialityCode}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.SpecialityCode, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
            ]
        })
    }),
})

export const {
    useGetSpecialityCodesQuery,
    useLazyGetSpecialityCodesQuery,
    useGetSpecialityCodeQuery,
    useLazyGetSpecialityCodeQuery,
    useCreateSpecialityCodeMutation,
    useUpdateSpecialityCodeMutation,
    useDeleteSpecialityCodeMutation,
} = specialityCodeApi