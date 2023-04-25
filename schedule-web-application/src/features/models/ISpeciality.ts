import {IDiscipline} from "./IDiscipline";

export interface ISpeciality {
    id: number
    code: string
    name: string
    disciplines: IDiscipline[]
    isDeleted: boolean
}