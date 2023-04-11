import {IDiscipline} from "./IDiscipline";
import {ITime} from "./ITime";

export interface IClassroomType {
    id: number
    name: string
}

export interface ILesson {
    id: number
    subgroup: number
    timetableId: number
    isChanged: boolean
    time: ITime
    discipline: IDiscipline
}