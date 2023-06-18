import {QueryFilter} from "../../common/enums/QueryFilter";
import {IPaginationQuery} from "./IPaginationQuery.ts";

export interface IPaginationQueryWithFilters extends IPaginationQuery {
    filter?: QueryFilter
    search?: string
}

