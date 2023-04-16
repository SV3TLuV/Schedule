import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid} from "../PaginationDataGrid";
import {useGetDaysQuery} from "../../services/dayApi";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40, type: "number" },
    { field: "name", headerName: "Название", width: 160, type: "string" },
    { field: "isStudy", headerName: "Учебный", width: 120, type: "boolean" },
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
]

export const DayEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data: list} = useGetDaysQuery(paginationQuery)

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