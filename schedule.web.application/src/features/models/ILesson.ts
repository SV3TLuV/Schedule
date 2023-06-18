import {ITime} from "./ITime";
import {IDiscipline} from "./IDiscipline";
import {ITeacherClassroom} from "./ITeacherClassroom.ts";

export interface ILesson {
    id: number
    number: number
    subgroup: number | null
    timetableId: number
    isChanged: boolean
    time: ITime | null
    discipline: IDiscipline | null
    teacherClassrooms: ITeacherClassroom[]
}