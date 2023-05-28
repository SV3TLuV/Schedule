import {IDay} from "./IDay";
import {ITerm} from "./ITerm";
import {IGroup} from "./IGroup";
import {IWeekType} from "./IWeekType";
import {ILessonTemplate} from "./ILessonTemplate.ts";

export interface ITemplate {
    id: number
    day: IDay
    term: ITerm
    groups: IGroup[]
    groupNames: string
    weekType: IWeekType
    lessons: ILessonTemplate[]
}