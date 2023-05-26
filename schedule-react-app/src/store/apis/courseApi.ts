import {ApiTags, baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {ICourse} from "../../features/models/ICourse.ts";
import {IPaginationQuery} from "../../features/queries/IPaginationQuery.ts";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums/HttpMethod.ts";

export const courseApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getCourses: builder.query<IPagedList<ICourse>, IPaginationQuery | void>({
            query: query => ({
                url: `${ApiTags.Course}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Course, id} as const)),
                {type: ApiTags.Course, id: 'LIST', page: result?.pageNumber}
            ]
        }),
        getCourse: builder.query<ICourse, number>({
            query: id => ({
                url: `${ApiTags.Course}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Course, id}]
        })
    })
})

export const {
    useGetCoursesQuery,
    useLazyGetCoursesQuery,
    useGetCourseQuery,
    useLazyGetCourseQuery,
} = courseApi