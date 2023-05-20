import {IPagedList} from "../../../features/models/IPagedList.ts";
import {IPaginationQueryWithFilters} from "../../../features/queries/IPaginationQueryWithFilters.ts";

export interface IEditor<T> {
    data: IPagedList<T> | undefined
    paginationQuery: IPaginationQueryWithFilters
    setPaginationQuery: (query: IPaginationQueryWithFilters) => void
}