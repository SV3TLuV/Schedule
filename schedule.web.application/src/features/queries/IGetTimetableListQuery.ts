import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IGetTimetableListQuery extends IPaginationQuery {
    groupId: number | null
    dateId: number | null
}