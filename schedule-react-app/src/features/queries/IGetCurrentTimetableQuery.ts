import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IGetCurrentTimetableQuery extends IPaginationQuery {
    groupId: number
    dateCount: number
}

