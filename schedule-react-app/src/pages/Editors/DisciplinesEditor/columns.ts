import {GridColDef} from "@mui/x-data-grid";
import {IDisciplineType} from "../../../features/models/IDisciplineType.ts";
import {ITerm} from "../../../features/models/ITerm.ts";
import {ISpeciality} from "../../../features/models/ISpeciality.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 120 },
    { field: 'code', headerName: 'Код', width: 120 },
    { field: 'totalHours', headerName: 'Кол-во часов', width: 120 },
    {
        field: 'type',
        headerName: 'Вид',
        width: 120,
        renderCell: props => (props.value as IDisciplineType).name
    },
    {
        field: 'term',
        headerName: 'Семестр',
        width: 120,
        renderCell: props => (props.value as ITerm).value
    },
    {
        field: 'speciality',
        headerName: 'Специальность',
        width: 140,
        renderCell: props => (props.value as ISpeciality).name
    }
]