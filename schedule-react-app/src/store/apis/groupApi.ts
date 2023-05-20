import {IPagedList} from "../../features/models/IPagedList.ts";
import {IGroup} from "../../features/models/IGroup.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";
import {ApiTags, baseApi} from "./baseApi.ts";
import {IPaginationQueryWithFilters} from "../../features/queries/IPaginationQueryWithFilters.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";

export const groupApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getGroups: builder.query<IPagedList<IGroup>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Group}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Group, id} as const)),
                {type: ApiTags.Group, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getGroup: builder.query<IGroup, number>({
            query: id => ({
                url: `${ApiTags.Group}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Group, id}]
        }),
        createGroup: builder.mutation<number, IGroup>({
            query: group => ({
                url: ApiTags.Group,
                method: HttpMethod.POST,
                body: {
                    number: group.number,
                    enrollmentYear: group.enrollmentYear,
                    termId: group.term.value,
                    specialityCodeId: group.speciality.id
                },
            }),
            invalidatesTags: id => [
                {type: ApiTags.Group, id},
                {type: ApiTags.Timetable, id: 'LIST'},
            ]
        }),
        updateGroup: builder.mutation<number, IGroup>({
            query: group => ({
                url: ApiTags.Group,
                method: HttpMethod.PUT,
                body: {
                    id: group.id,
                    number: group.number,
                    enrollmentYear: group.enrollmentYear,
                    termId: group.term.value,
                    specialityCodeId: group.speciality.id
                },
            }),
            invalidatesTags: id => [
                {type: ApiTags.Group, id},
                {type: ApiTags.Timetable, id: 'LIST'},
            ]
        }),
        deleteGroup: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Group}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: id => [
                {type: ApiTags.Group, id},
                {type: ApiTags.Timetable, id: 'LIST'},
            ]
        })
    }),
})

export const {
    useGetGroupsQuery,
    useLazyGetGroupsQuery,
    useGetGroupQuery,
    useLazyGetGroupQuery,
    useCreateGroupMutation,
    useUpdateGroupMutation,
    useDeleteGroupMutation
} = groupApi