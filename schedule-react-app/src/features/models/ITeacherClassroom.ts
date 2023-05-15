import {IClassroom} from "./IClassroom.ts";
import {ITeacher} from "./ITeacher.ts";

export interface ITeacherClassroom {
    teacher: ITeacher
    classroom: IClassroom
}