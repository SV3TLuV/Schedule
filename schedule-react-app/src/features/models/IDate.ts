import {IDay} from "./IDay";
import {IWeekType} from "./IWeekType";

export interface IDate {
    id: number
    isStudy: boolean
    term: number
    value: string
    day: IDay
    weekType: IWeekType
}