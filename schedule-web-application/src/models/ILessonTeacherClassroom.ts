import {ITeacher} from "./ITeacher";
import {IClassroom} from "./IClassroom";
import {ILesson} from "./IClassroomType";

export interface ILessonTeacherClassroom {
    lesson: ILesson,
    teacher: ITeacher,
    classroom: IClassroom
}