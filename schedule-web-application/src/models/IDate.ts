import {IDay} from "./IDay";
import {ITimeType} from "./ITimeType";
import {IWeekType} from "./IWeekType";

export interface IDate {
    id: number
    isStudy: boolean
    term: number
    value: Date
    day: IDay
    timeType: ITimeType
    weekType: IWeekType
}