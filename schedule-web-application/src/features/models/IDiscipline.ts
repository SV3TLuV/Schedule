import {ITerm} from "./ITerm";
import {ISpecialityCode} from "./ISpecialityCode";

export interface IDiscipline {
    id: number
    name: string
    code: string
    totalHours: number
    isDeleted: boolean
    term: ITerm
    specialityCode: ISpecialityCode
}