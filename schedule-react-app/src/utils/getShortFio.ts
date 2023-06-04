import {ITeacher} from "../features/models";

export const getShortFio = (teacher: ITeacher | undefined) => {
    if (!teacher)
        return ''
    return `${teacher.surname} ${teacher.name[0]}. ${teacher.middleName[0]}.`
}