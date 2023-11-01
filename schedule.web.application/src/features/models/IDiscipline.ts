import {ITerm} from "./ITerm";
import {ISpeciality} from "./ISpeciality";
import {IDisciplineType} from "./IDisciplineType";
import {IDisciplineName} from "./IDisciplineName.ts";
import {IDisciplineCode} from "./IDisciplineCode.ts";

export interface IDiscipline {
    id: number
    name: IDisciplineName
    code: IDisciplineCode
    totalHours: number
    isDeleted: boolean
    type: IDisciplineType
    term: ITerm
    speciality: ISpeciality
}