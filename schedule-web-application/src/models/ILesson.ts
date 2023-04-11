import {ITime} from "./ITime";
import {IDiscipline} from "./IDiscipline";
import {ILessonTeacherClassroom} from "./ILessonTeacherClassroom";

export interface ILesson {
    id: number
    subgroup: number
    timetableId: number
    isChanged: boolean
    time: ITime
    discipline: IDiscipline
    teacherClassrooms: ILessonTeacherClassroom[]
}