import {ITime} from "./ITime.ts";
import {IDiscipline} from "./IDiscipline.ts";
import {ITeacherClassroom} from "./ITeacherClassroom.ts";

export interface ILessonTemplate {
    id: number
    number: number
    subgroup: number
    templateId: number
    time: ITime
    discipline: IDiscipline
    teacherClassrooms: ITeacherClassroom[]
}
