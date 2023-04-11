import {ITimeType} from "./ITimeType";

export interface ITime {
    id: number
    start: string
    end: string
    lessonNumber: number
    type: ITimeType
    isDeleted: boolean
}