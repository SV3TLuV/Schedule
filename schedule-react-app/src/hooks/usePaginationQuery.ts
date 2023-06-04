import {useState} from "react";
import {QueryFilter} from "../common/enums";
import {IPaginationQueryWithFilters} from "../features/queries";

interface IUsePaginationQuery {
    filter?: QueryFilter
    search?: string
    pageSize?: number
    page?: number
}

export const usePaginationQuery = (
    {
        filter = QueryFilter.Available,
        search = '',
        pageSize = 20,
        page = 1
    }: IUsePaginationQuery = {}) => {
    
    const [paginationQuery, setPaginationQuery] = useState(() => ({
        filter,
        pageSize,
        page,
        search
    } as IPaginationQueryWithFilters))

    return [paginationQuery, setPaginationQuery] as const;
}