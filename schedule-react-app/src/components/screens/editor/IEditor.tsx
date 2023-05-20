import {IPagedList} from "../../../features/models/IPagedList.ts";
import {IPaginationQueryWithFilters} from "../../../features/queries/IPaginationQueryWithFilters.ts";

export interface IEditor<T> {
    onCreate?: () => void;
    onUpdate?: () => void;
    onDelete?: () => void;
    onRestore?: () => void;
    data: IPagedList<T> | undefined
    paginationQuery: IPaginationQueryWithFilters
    setPaginationQuery: (query: IPaginationQueryWithFilters) => void
}