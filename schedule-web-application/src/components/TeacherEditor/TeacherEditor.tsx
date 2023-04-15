import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", hideable: true },
    { field: "surname", headerName: "Фамилия", width: 120 },
    { field: "name", headerName: "Имя", width: 120 },
    { field: "middleName", headerName: "Отчество", width: 120 },
    { field: "email", headerName: "Почта", width: 120 },
    {
        field: "change",
        headerName: "Изменить",
        renderCell: params => (
            <Button>
                Изменить
            </Button>
        )
    },
    {
        field: "delete",
        headerName: "Удалить",
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