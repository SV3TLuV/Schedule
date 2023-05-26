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
            providesTags: (result, _, arg) => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Group, id} as const)),
                {
                    type: ApiTags.Group,
                    id: 'LIST',
                    page: result?.pageNumber,
                    search: arg?.search
                }
            ]
        }),
        getGroup: builder.query<IGroup, number>({
            query: id => ({
                url: `${ApiTags.Group}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Group, id}]
        }),
        restoreGroup: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Group}/restore`,
                method: HttpMethod.POST,
                body: {
                    id: id
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Group },
                {type: ApiTags.Timetable },
                {type: ApiTags.Template },
            ]
        }),
        createGroup: builder.mutation<number, IGroup>({
            query: group => ({
                url: ApiTags.Group,
                method: HttpMethod.POST,
                body: {
                    number: group.number.toString(),
                    enrollmentYear: group.enrollmentYear,
                    termId: group.term.id,
                    specialityId: group.speciality.id,
                    mergedGroupIds: (group.mergedGroups ?? [])
                        .map(group => group.id)
                },
            }),
            invalidatesTags: () => [
                {type: ApiTags.Group},
                {type: ApiTags.Timetable },
                {type: ApiTags.Template },
            ]
        }),
        updateGroup: builder.mutation<number, IGroup>({
            query: group => ({
                url: ApiTags.Group,
                method: HttpMethod.PUT,
                body: {
                    id: group.id,
                    number: group.number.toString(),
                    enrollmentYear: group.enrollmentYear,
                    termId: group.term.id,
                    specialityId: group.speciality.id,
                    mergedGroupIds: (group.mergedGroups ?? [])
                        .map(group => group.id)
                },
            }),
            invalidatesTags: () => [
                {type: ApiTags.Group},
                {type: ApiTags.Timetable },
                {type: ApiTags.Template },
            ]
        }),
        deleteGroup: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Group}/${id}`,
                method: HttpMethod.DELETE
            }),
            invalidatesTags: () => [
                {type: ApiTags.Group},
                {type: ApiTags.Timetable },
                {type: ApiTags.Template },
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
    useRestoreGroupMutation,
    useUpdateGroupMutation,
    useDeleteGroupMutation
} = groupApi