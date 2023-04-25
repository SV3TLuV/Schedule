import {ITerm} from "./ITerm";
import {ISpeciality} from "./ISpeciality";

export interface IDiscipline {
    id: number
    name: string
    code: string
    totalHours: number
    isDeleted: boolean
    term: ITerm
    speciality: ISpeciality
}