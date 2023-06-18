import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IGetCurrentTimetableQuery extends IPaginationQuery {
    groupId: number | null
    dateCount: number | null
}

