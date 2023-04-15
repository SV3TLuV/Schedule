import {IDiscipline} from "./IDiscipline";
import {IGroup} from "./IGroup";

export interface ITeacher {
    id: number
    name: string
    surname: string
    middleName: string
    email: string
    groups: IGroup[]
    disciplines: IDiscipline[]
    isDeleted: boolean
}