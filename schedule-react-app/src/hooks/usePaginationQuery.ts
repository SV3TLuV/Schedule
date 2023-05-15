import {useState} from "react";
import {IPaginatedQueryWithFilter} from "../features/queries/IPaginatedQueryWithFilter";
import {QueryFilter} from "../common/enums/QueryFilter";

export const usePaginationQuery = (filter = QueryFilter.Available, pageSize = 20, page = 1) => {
    const [paginationModel, setPaginationModel] = useState({
        filter,
        pageSize,
        page
    } as IPaginatedQueryWithFilter)

    return [paginationModel, setPaginationModel] as const;
}