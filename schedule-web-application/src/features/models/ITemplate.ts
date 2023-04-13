import {IDay} from "./IDay";
import {ITerm} from "./ITerm";
import {IGroup} from "./IGroup";
import {IWeekType} from "./IWeekType";
import {ILesson} from "./ILesson";

export interface ITemplate {
    id: number
    day: IDay
    term: ITerm
    group: IGroup
    weekType: IWeekType
    lessons: ILesson[]
}