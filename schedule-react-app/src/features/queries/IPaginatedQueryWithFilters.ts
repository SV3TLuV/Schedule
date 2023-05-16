import {QueryFilter} from "../../common/enums/QueryFilter";
import {IPaginatedQuery} from "./IPaginatedQuery";

export interface IPaginatedQueryWithFilters extends IPaginatedQuery {
    filter?: QueryFilter
    search?: string
}

