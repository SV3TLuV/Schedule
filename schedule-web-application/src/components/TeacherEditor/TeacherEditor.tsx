import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {useGetTeachersQuery} from "../../services/teacherApi";
import {useCallback} from "react";
import {PaginationDataGrid} from "../PaginationDataGrid";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", hideable: true },
    { field: "surname", headerName: "Фамилия", width: 120 },
    { field: "name", headerName: "Имя", width: 120 },
    { field: "middleName", headerName: "Отчество", width: 120 },
    { field: "email", headerName: "Почта", width: 240 },
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

const toolbar = () => (
    <GridToolbarContainer>
        <Button>
            Добавить
        </Button>
    </GridToolbarContainer>
)

interface ITeacherEditorProps {
    filter: QueryFilter
}

export const TeacherEditor = ({filter}: ITeacherEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTeachersQuery(paginationQuery)

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