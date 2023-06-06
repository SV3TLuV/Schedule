import {HttpTransportType, HubConnectionBuilder, LogLevel, signalMiddleware, withCallbacks} from "redux-signalr";
import {ApiTags} from "./apis/apiTags.ts";
import {
    classroomApi,
    courseApi,
    dateApi,
    dayApi,
    disciplineApi,
    disciplineTypeApi,
    groupApi,
    lessonApi,
    lessonTemplateApi,
    roleApi,
    specialityApi,
    teacherApi,
    templateApi,
    termApi,
    timeApi,
    timetableApi, timeTypeApi, userApi, weekTypeApi
} from "./apis";
import {AppDispatch, AppState} from "./store.ts";
import {API_URL} from "../configuration.ts";

const connection = new HubConnectionBuilder()
    .withUrl(`${API_URL}/hub/notification`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
    })
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

connection.start()

const callbacks = withCallbacks<AppDispatch, AppState>()
    .add('notified', (objName: string) =>
        (dispatch) => {

        switch (objName as ApiTags) {
            case ApiTags.Classroom:
                dispatch(classroomApi.util.invalidateTags([
                    {type: ApiTags.Classroom},
                    {type: ApiTags.Lesson}
                ]))
                break
            case ApiTags.Course:
                dispatch(courseApi.util.invalidateTags([
                    {type: ApiTags.Course}
                ]))
                break
            case ApiTags.Date:
                dispatch(dateApi.util.invalidateTags([
                    {type: ApiTags.Date}
                ]))
                break
            case ApiTags.Day:
                dispatch(dayApi.util.invalidateTags([
                    {type: ApiTags.Day},
                    {type: ApiTags.Date},
                    {type: ApiTags.Template},
                ]))
                break
            case ApiTags.Discipline:
                dispatch(disciplineApi.util.invalidateTags([
                    { type: ApiTags.Discipline },
                    { type: ApiTags.Lesson }
                ]))
                break
            case ApiTags.DisciplineType:
                dispatch(disciplineTypeApi.util.invalidateTags([
                    {type: ApiTags.DisciplineType}
                ]))
                break
            case ApiTags.Group:
                dispatch(groupApi.util.invalidateTags([
                    {type: ApiTags.Group},
                    {type: ApiTags.Timetable },
                    {type: ApiTags.Template },
                ]))
                break
            case ApiTags.Lesson:
                dispatch(lessonApi.util.invalidateTags([
                    {type: ApiTags.Lesson },
                    {type: ApiTags.LessonNumber },
                    {type: ApiTags.Timetable },
                ]))
                break
            case ApiTags.LessonTemplate:
                dispatch(lessonTemplateApi.util.invalidateTags([
                    {type: ApiTags.LessonTemplate },
                    {type: ApiTags.Template },
                    {type: ApiTags.Lesson },
                    {type: ApiTags.LessonNumber },
                    {type: ApiTags.Timetable },
                ]))
                break
            case ApiTags.Role:
                dispatch(roleApi.util.invalidateTags([
                    {type: ApiTags.Role},
                    {type: ApiTags.User},
                ]))
                break
            case ApiTags.Speciality:
                dispatch(specialityApi.util.invalidateTags([
                    {type: ApiTags.Speciality},
                    {type: ApiTags.Group},
                    {type: ApiTags.Discipline},
                ]))
                break
            case ApiTags.Teacher:
                dispatch(teacherApi.util.invalidateTags([
                    {type: ApiTags.Teacher},
                    {type: ApiTags.Lesson},
                ]))
                break
            case ApiTags.Template:
                dispatch(templateApi.util.invalidateTags([
                    {type: ApiTags.Template},
                ]))
                break
            case ApiTags.Term:
                dispatch(termApi.util.invalidateTags([
                    {type: ApiTags.Term}
                ]))
                break
            case ApiTags.Time:
                dispatch(timeApi.util.invalidateTags([
                    {type: ApiTags.Time},
                    {type: ApiTags.Lesson},
                ]))
                break
            case ApiTags.Timetable:
                dispatch(timetableApi.util.invalidateTags([
                    {type: ApiTags.Timetable}
                ]))
                break
            case ApiTags.TimeType:
                dispatch(timeTypeApi.util.invalidateTags([
                    {type: ApiTags.TimeType},
                    {type: ApiTags.Time},
                ]))
                break
            case ApiTags.User:
                dispatch(userApi.util.invalidateTags([
                    {type: ApiTags.User}
                ]))
                break
            case ApiTags.WeekType:
                dispatch(weekTypeApi.util.invalidateTags([
                    {type: ApiTags.WeekType}
                ]))
                break
        }
    })

export const signalRMiddleware = signalMiddleware({
    callbacks,
    connection
})