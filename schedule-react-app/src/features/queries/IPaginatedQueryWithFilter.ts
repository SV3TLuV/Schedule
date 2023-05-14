import {QueryFilter} from "../../common/enums/QueryFilter";
import {IPaginatedQuery} from "./IPaginatedQuery";

export interface IPaginatedQueryWithFilter extends IPaginatedQuery {
    filter?: QueryFilter
}