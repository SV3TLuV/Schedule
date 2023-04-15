import {GridColDef} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";


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
]

export const GroupEditor = () => {
    return (
        <h1>Едитор групп</h1>
    )
}