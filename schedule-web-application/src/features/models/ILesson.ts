import {ITime} from "./ITime";
import {IDiscipline} from "./IDiscipline";
import {ITeacherClassroom} from "@/features/models/ITeacherClassroom";

export interface ILesson {
    id: number
    number: number
    subgroup: number
    timetableId: number
    isChanged: boolean
    time: ITime
    discipline: IDiscipline
    teacherClassrooms: ITeacherClassroom[]
}