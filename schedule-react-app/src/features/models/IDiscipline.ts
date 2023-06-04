import {ITerm} from "./ITerm";
import {ISpeciality} from "./ISpeciality";
import {IDisciplineType} from "./IDisciplineType";

export interface IDiscipline {
    id: number
    name: string
    code: string
    totalHours: number
    isDeleted: boolean
    type: IDisciplineType
    term: ITerm
    speciality: ISpeciality
}