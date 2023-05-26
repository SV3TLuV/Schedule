import {ISpeciality} from "./ISpeciality";
import {ITerm} from "./ITerm";

export interface IGroup {
    id: number
    name: string
    number: string
    enrollmentYear: number
    term: ITerm
    speciality: ISpeciality
    mergedGroups: IGroup[] | undefined
    isDeleted: boolean
}