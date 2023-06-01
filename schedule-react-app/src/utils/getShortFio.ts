import {ITeacher} from "../features/models/ITeacher.ts";

export const getShortFio = (teacher: ITeacher | undefined) => {
    if (!teacher)
        return ''
    return `${teacher.surname} ${teacher.name[0]}. ${teacher.middleName[0]}.`
}