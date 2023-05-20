import {useState} from "react";
import {QueryFilter} from "../common/enums/QueryFilter";
import {IPaginationQueryWithFilters} from "../features/queries/IPaginationQueryWithFilters.ts";

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
    } as IPaginationQueryWithFilters)

    return [paginationQuery, setPaginationQuery] as const;
}