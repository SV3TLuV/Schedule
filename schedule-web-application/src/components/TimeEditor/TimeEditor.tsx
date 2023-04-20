import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {useGetTimesQuery} from "../../services/timeApi";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {ITimeType} from "../../features/models/ITimeType";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40 },
    { field: "start", headerName: "Начало", width: 100 },
    { field: "end", headerName: "Конец", width: 100 },
    { field: "lessonNumber", headerName: "Номер пары", width: 140 },
    {
        field: "type",
        headerName: "Вид",
        width: 160,
        renderCell: params => (params.value as ITimeType).name
    },
]

interface ITimeEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const TimeEditor = ({ filter, type }: ITimeEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTimesQuery(paginationQuery)

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