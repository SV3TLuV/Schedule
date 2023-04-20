import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {useGetTimeTypesQuery} from "../../services/timeTypeApi";
import {QueryFilter} from "../../common/enums/QueryFilter";


const columns: GridColDef[] = [
    {
        field: "id",
        headerName: "ID",
        width: 40,
        type: "number",
    },
    {
        field: "name",
        headerName: "Название",
        width: 160,
        type: "string",
    },
]

interface ITimeTypeEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const TimeTypeEditor = ({ filter, type }: ITimeTypeEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTimeTypesQuery(paginationQuery)

    return (
        <PaginationDataGrid
            columns={columns}
            list={list}
            type={type}
            paginationModel={paginationQuery}
            onPaginationModelChange={setPaginationQuery}
        />
    )
}