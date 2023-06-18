import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IGetTemplateListQuery extends IPaginationQuery {
    weekTypeId: number | null
    termId: number | null
    dayId: number | null
    groupId: number | null
}