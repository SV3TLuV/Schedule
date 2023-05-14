import {ApiTags, baseApi, buildUrlArguments} from "./baseApi.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ISpeciality} from "../../features/models/ISpeciality.ts";
import {IPaginatedQueryWithFilter} from "../../features/queries/IPaginatedQueryWithFilter.ts";

export const specialityApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getSpecialities: builder.query<IPagedList<ISpeciality>, IPaginatedQueryWithFilter | void>({
            query: query => ({
                url: `${ApiTags.Speciality}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Speciality, id} as const)),
                {type: ApiTags.Speciality, id: "LIST", page: result?.pageNumber}
            ]
        }),
        getSpeciality: builder.query<ISpeciality, number>({
            query: id => ({
                url: `${ApiTags.Speciality}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Speciality, id}
            ]
        }),
        createSpeciality: builder.mutation<number, ISpeciality>({
            query: speciality => ({
                url: ApiTags.Speciality,
                method: HttpMethod.POST,
                body: {
                    code: speciality.code,
                    name: speciality.name,
                    disciplineIds: speciality.disciplines.map(d => d.id),
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Speciality, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
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
                    disciplineIds: speciality.disciplines.map(d => d.id),
                    isDeleted: speciality.isDeleted,
                }
            }),
            invalidatesTags: id => [
                {type: ApiTags.Speciality, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
            ]
        }),
        deleteSpeciality: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Speciality}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Speciality, id},
                {type: ApiTags.Group, id: "LIST"},
                {type: ApiTags.Discipline, id: "LIST"},
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
    useUpdateSpecialityMutation,
    useDeleteSpecialityMutation,
} = specialityApi