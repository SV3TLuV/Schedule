import {IDiscipline} from "./IDiscipline";

export interface ISpeciality {
    id: number
    code: string
    name: string
    maxTermId: number
    disciplines: IDiscipline[]
    isDeleted: boolean
}