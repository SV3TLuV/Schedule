import {baseApi} from "./baseApi.ts";
import {IPagedList} from "../../features/models";
import {ILesson} from "../../features/models";
import {IPaginationQueryWithFilters} from "../../features/queries";
import {buildUrlArguments} from "../../utils/buildUrlArguments.ts";
import {HttpMethod} from "../../common/enums";
import {IUpdateFilledLessonsTimeCommand} from "../../features/commands";
import {ApiTags} from "./apiTags.ts";

export const lessonApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getLessons: builder.query<IPagedList<ILesson>, IPaginationQueryWithFilters | void>({
            query: query => ({
                url: `${ApiTags.Lesson}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Lesson, id} as const)),
                {
                    type: ApiTags.Lesson,
                    id: 'LIST',
                    page: result?.pageNumber,
                }
            ]
        }),
        getLesson: builder.query<ILesson, number>({
            query: id => ({
                url: `${ApiTags.Lesson}/${id}`,
                method: HttpMethod.GET
            }),
            providesTags: (_, __, id) => [{type: ApiTags.Lesson, id}]
        }),
        getLessonNumbers: builder.query<number[], number>({
            query: dateId => ({
                url: `${ApiTags.Lesson}/number/${dateId}`,
                method: HttpMethod.GET
            }),
            providesTags: () => [{type: ApiTags.LessonNumber}]
        }),
        createLesson: builder.mutation<number, ILesson>({
            query: lesson => ({
                url: ApiTags.Lesson,
                method: HttpMethod.POST,
                body: {
                    number: lesson.number,
                    subgroup:  lesson.subgroup ? Number(lesson.subgroup) : null,
                    timetableId: lesson.timetableId,
                    timeId: lesson.time?.id ?? null,
                    disciplineId: lesson.discipline?.id ?? null,
                    teacherClassroomIds: lesson.teacherClassrooms
                        .map(teacherClassroom => ({
                            teacherId: teacherClassroom.teacher?.id,
                            classroomId: teacherClassroom.classroom?.id,
                        })),
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Lesson },
                {type: ApiTags.LessonNumber },
                {type: ApiTags.Timetable },
            ]
        }),
        updateFilledLessonsTime: builder.mutation<number, IUpdateFilledLessonsTimeCommand>({
            query: command => ({
                url: `${ApiTags.Lesson}/update-filled-lessons-time`,
                method: HttpMethod.POST,
                body: command
            }),
            invalidatesTags: () => [
                {type: ApiTags.Lesson },
                {type: ApiTags.Timetable },
            ]
        }),
        updateLesson: builder.mutation<number, ILesson>({
            query: lesson => ({
                url: ApiTags.Lesson,
                method: HttpMethod.PUT,
                body: {
                    id: lesson.id,
                    number: lesson.number,
                    subgroup: lesson.subgroup ? Number(lesson.subgroup) : null,
                    timetableId: lesson.timetableId,
                    timeId: lesson.time?.id ?? null,
                    disciplineId: lesson.discipline?.id ?? null,
                    teacherClassroomIds: lesson.teacherClassrooms
                        .map(teacherClassroom => ({
                            teacherId: teacherClassroom.teacher?.id,
                            classroomId: teacherClassroom.classroom?.id,
                        })),
                }
            }),
            invalidatesTags: () => [
                {type: ApiTags.Lesson },
                {type: ApiTags.Timetable },
            ]
        }),
        deleteLesson: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Lesson}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: () => [
                {type: ApiTags.Lesson },
                {type: ApiTags.LessonNumber },
                {type: ApiTags.Timetable },
            ]
        }),
    })
})

export const {
    useGetLessonsQuery,
    useLazyGetLessonsQuery,
    useGetLessonQuery,
    useLazyGetLessonQuery,
    useGetLessonNumbersQuery,
    useLazyGetLessonNumbersQuery,
    useCreateLessonMutation,
    useUpdateFilledLessonsTimeMutation,
    useUpdateLessonMutation,
    useDeleteLessonMutation,
} = lessonApi