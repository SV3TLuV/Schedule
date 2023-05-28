import {ITime} from "./ITime.ts";
import {IDiscipline} from "./IDiscipline.ts";
import {ITeacherClassroom} from "./ITeacherClassroom.ts";

export interface ILessonTemplate {
    id: number
    number: number
    subgroup: number | null
    templateId: number
    time: ITime | null
    discipline: IDiscipline | null
    teacherClassrooms: ITeacherClassroom[]
}
