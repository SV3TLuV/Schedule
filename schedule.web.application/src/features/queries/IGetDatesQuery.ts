import {IPaginationQueryWithFilters} from "./IPaginationQueryWithFilters.ts";

export interface IGetDatesQuery extends IPaginationQueryWithFilters {
    educationalOnly: boolean | null
}