import {ICourse} from "./ICourse";
import {ISpeciality} from "./ISpeciality";

export interface IGroup {
    id: number
    name: string
    number: string
    enrollmentYear: number
    course: ICourse
    speciality: ISpeciality
    mergedGroups: IGroup[]
    isDeleted: boolean
}