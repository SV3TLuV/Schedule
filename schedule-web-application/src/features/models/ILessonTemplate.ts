import {ITime} from "@/features/models/ITime";
import {IDiscipline} from "@/features/models/IDiscipline";
import {ITeacherClassroom} from "@/features/models/ITeacherClassroom";

export interface ILessonTemplate {
    id: number
    number: number
    subgroup: number
    templateId: number
    time: ITime
    discipline: IDiscipline
    teacherClassrooms: ITeacherClassroom[]
}
