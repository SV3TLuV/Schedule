import {IDiscipline} from "./IDiscipline";

export interface ISpecialityCode {
    id: number
    code: string
    name: string
    disciplines: IDiscipline[]
    isDeleted: boolean
}