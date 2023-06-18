import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";
import {ILessonTemplate} from "../../features/models";
import {ApiTags} from "./apiTags.ts";

export const lessonTemplateApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getLessonTemplates: builder.query<IPagedList<ILessonTemplate>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.LessonTemplate}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.LessonTemplate, id} as const)),
                {
                    type: ApiTags.LessonTemplate,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getLessonTemplate: builder.query<ILessonTemplate, number>({
            query: id => ({
                url: `${ApiTags.LessonTemplate}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (_, __, id) => [{type: ApiTags.LessonTemplate, id}]
        }),
        createLessonTemplate: builder.mutation<number, ILessonTemplate>({
            query: lessonTemplate => ({
                url: ApiTags.LessonTemplate,
                method: HttpMethod.POST,
                body: {
                    number: lessonTemplate.number,
                    subgroup: lessonTemplate.subgroup,
                    templateId: lessonTemplate.templateId,
                    timeId: lessonTemplate.time?.id ?? null,
                    disciplineId: lessonTemplate.discipline?.id ?? null,
                    teacherClassroomIds: lessonTemplate.teacherClassrooms
                        .map(teacherClassroom => ({
                            teacherId: teacherClassroom.teacher?.id,
                            classroomId: teacherClassroom.classroom?.id,
                        })),
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.LessonTemplate },
                {type: ApiTags.Template },
                {type: ApiTags.Lesson },
                {type: ApiTags.Timetable },
            ]
        }),
        updateLessonTemplate: builder.mutation<number, ILessonTemplate>({
            query: lessonTemplate => ({
                url: ApiTags.LessonTemplate,
                method: HttpMethod.PUT,
                body: {
                    id: lessonTemplate.id,
                    number: lessonTemplate.number,
                    subgroup: lessonTemplate.subgroup,
                    templateId: lessonTemplate.templateId,
                    timeId: lessonTemplate.time?.id ?? null,
                    disciplineId: lessonTemplate.discipline?.id ?? null,
                    teacherClassroomIds: lessonTemplate.teacherClassrooms
                        .map(teacherClassroom => ({
                            teacherId: teacherClassroom.teacher?.id,
                            classroomId: teacherClassroom.classroom?.id,
                        })),
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.LessonTemplate },
                {type: ApiTags.Template },
                {type: ApiTags.Lesson },
                {type: ApiTags.LessonNumber },
                {type: ApiTags.Timetable },
            ]
        }),
        deleteLessonTemplate: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.LessonTemplate}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                {type: ApiTags.LessonTemplate },
                {type: ApiTags.Template },
                {type: ApiTags.Lesson },
                {type: ApiTags.LessonNumber },
                {type: ApiTags.Timetable },
            ]
        }),
    })
})

export const {
    useGetLessonTemplatesQuery,
    useLazyGetLessonTemplatesQuery,
    useGetLessonTemplateQuery,
    useLazyGetLessonTemplateQuery,
    useCreateLessonTemplateMutation,
    useUpdateLessonTemplateMutation,
    useDeleteLessonTemplateMutation,
} = lessonTemplateApi