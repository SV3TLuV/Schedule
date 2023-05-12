import {IPaginatedQuery} from "./IPaginatedQuery";
import {QueryFilter} from "@/enums/QueryFilter";

export interface IPaginatedQueryWithFilter extends IPaginatedQuery {
    filter?: QueryFilter
}