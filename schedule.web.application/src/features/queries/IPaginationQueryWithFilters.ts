import {QueryFilter} from "../../common/enums";
import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IPaginationQueryWithFilters extends IPaginationQuery {
    filter?: QueryFilter
    search?: string
}

