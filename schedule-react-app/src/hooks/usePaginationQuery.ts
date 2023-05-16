import {useState} from "react";
import {QueryFilter} from "../common/enums/QueryFilter";
import {IPaginatedQueryWithFilters} from "../features/queries/IPaginatedQueryWithFilters.ts";

export const usePaginationQuery = (
    filter = QueryFilter.Available,
    search = '',
    pageSize = 20,
    page = 1) => {
    
    const [paginationQuery, setPaginationQuery] = useState({
        filter,
        pageSize,
        page,
        search
    } as IPaginatedQueryWithFilters)

    return [paginationQuery, setPaginationQuery] as const;
}