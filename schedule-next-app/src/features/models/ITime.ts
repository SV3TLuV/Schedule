import {ITimeType} from "./ITimeType";

export interface ITime {
    id: number
    start: string
    end: string
    duration: number
    lessonNumber: number
    type: ITimeType
    isDeleted: boolean
}