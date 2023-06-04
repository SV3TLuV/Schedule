import {IClassroomType} from "./IClassroomType";

export interface IClassroom {
    id: number
    cabinet: string
    types: IClassroomType[]
    isDeleted: boolean
}