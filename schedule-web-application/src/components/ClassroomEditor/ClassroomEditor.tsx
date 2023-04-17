import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useGetClassroomsQuery} from "../../services/classroomApi";
import {useCallback} from "react";
import {PaginationDataGrid} from "../PaginationDataGrid";
import {IClassroomType} from "../../features/models/IClassroomType";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40 },
    { field: "cabinet", headerName: "Кабинет", width: 120 },
    {
        field: "types",
        headerName: "Виды",
        width: 200,
        renderCell: params =>
            (params.value as IClassroomType[])
                .map(type => type.name)
                .join(", ")
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

interface IClassroomEditorProps {
    filter: QueryFilter
}

export const ClassroomEditor = ({filter}: IClassroomEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetClassroomsQuery(paginationQuery)

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