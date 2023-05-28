import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IGetTimetableListQuery extends IPaginationQuery {
    groupId: number
    dateId: number
}