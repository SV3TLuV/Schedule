import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid} from "../PaginationDataGrid";
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
    {
        field: "change",
        headerName: "Изменить",
        sortable: false,
        width: 120,
        renderCell: params => (
            <Button>
                Изменить
            </Button>
        ),
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
        ),
    },
]

interface ITimeTypeEditorProps {
    filter: QueryFilter
}

export const TimeTypeEditor = ({ filter }: ITimeTypeEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTimeTypesQuery(paginationQuery)

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