import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid} from "../PaginationDataGrid";
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
    {
        field: "change",
        headerName: "Изменить",
        sortable: false,
        width: 120,
        renderCell: params => (
            <Button>
                Изменить
            </Button>
        )
    },
    {
        field: "delete",
        headerName: "Удалить",
        sortable: false,
        width: 120,
        renderCell: params => (
            <Button>
                Удалить
            </Button>
        )
    }
]

interface ITimeEditorProps {
    filter: QueryFilter
}

export const TimeEditor = ({ filter }: ITimeEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTimesQuery(paginationQuery)

    const toolbar = useCallback(() => (
        <GridToolbarContainer style={{ padding: "20px" }}>
            <Button>
                Добавить
            </Button>
        </GridToolbarContainer>
    ), [])

    return (
        <PaginationDataGrid
            columns={columns}
            list={list}
            components={{ Toolbar: toolbar }}
            paginationModel={paginationQuery}
            onPaginationModelChange={setPaginationQuery}
        />
    )
}