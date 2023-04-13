import {IDate} from "./IDate";
import {IGroup} from "./IGroup";
import {ILesson} from "./ILesson";

export interface ITimetable {
    id: number
    date: IDate
    group: IGroup
    lessons: ILesson[]
}