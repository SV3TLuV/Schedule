import {IGroup} from "./IGroup.ts";
import {IGrouping} from "./IGrouping.ts";
import {IDate} from "./IDate.ts";
import {ITimetable} from "./ITimetable.ts";

export interface ICurrentTimetable {
    groups: IGroup[]
    dates: IGrouping<IDate, ITimetable>
}