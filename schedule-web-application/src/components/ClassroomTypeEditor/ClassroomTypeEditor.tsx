import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {PaginationDataGrid} from "../PaginationDataGrid";
import {useGetClassroomTypesQuery} from "../../services/classroomTypeApi";


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

export const ClassroomTypeEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data: list} = useGetClassroomTypesQuery(paginationQuery)

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