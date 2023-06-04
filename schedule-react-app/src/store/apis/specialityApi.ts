import {baseApi} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums";
import {IPagedList} from "../../features/models";
import {ISpeciality} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {ApiTags} from "./apiTags.ts";

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
        })
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
} = specialityApi