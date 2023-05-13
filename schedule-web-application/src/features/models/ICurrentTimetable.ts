import {IGroup} from "@/features/models/IGroup";
import {ITimetable} from "@/features/models/ITimetable";
import {IDate} from "@/features/models/IDate";
import {IGrouping} from "@/features/models/IGrouping";

export interface ICurrentTimetable {
    groups: IGroup[]
    dates: IGrouping<IDate, ITimetable>
}