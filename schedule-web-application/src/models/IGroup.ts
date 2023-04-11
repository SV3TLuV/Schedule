import {ICourse} from "./ICourse";
import {ISpecialityCode} from "./ISpecialityCode";

export interface IGroup {
    id: number
    name: string
    enrollmentYear: number
    course: ICourse
    specialityCode: ISpecialityCode
    isDeleted: boolean
}