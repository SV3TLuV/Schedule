import {ICourse} from "./ICourse";
import {ISpecialityCode} from "./ISpecialityCode";

export interface IGroup {
    id: number
    name: string
    number: string
    enrollmentYear: number
    course: ICourse
    specialityCode: ISpecialityCode
    mergedGroups: IGroup[]
    isDeleted: boolean
}