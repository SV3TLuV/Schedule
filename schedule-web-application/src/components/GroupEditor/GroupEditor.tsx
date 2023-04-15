import {DataGrid, GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";
import {IGroup} from "../../features/models/IGroup";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", hideable: true },
    { field: "name", headerName: "Название", width: 160 },
    { field: "enrollmentYear", headerName: "Год поступления", width: 160 },
    {
        field: "course",
        headerName: "Курс",
        width: 60,
        renderCell: params => (params.value as ICourse).id
    },
    {
        field: "specialityCode",
        headerName: "Код специальности",
        width:180,
        renderCell: params => (params.value as ISpecialityCode).code
    },
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

interface IGroupEditorProps {
    filter: "available" | "deleted"
}

export const GroupEditor = ({ filter }: IGroupEditorProps) => {
    return (
        <div className="GroupEditor">
            <DataGrid
                columns={columns}
                rows={[]}
                sx={{ height: "100vh" }}
            />
        </div>
    )
}