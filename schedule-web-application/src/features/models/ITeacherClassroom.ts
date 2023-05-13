import {ITeacher} from "@/features/models/ITeacher";
import {IClassroom} from "@/features/models/IClassroom";

export interface ITeacherClassroom {
    teacher: ITeacher
    classroom: IClassroom
}