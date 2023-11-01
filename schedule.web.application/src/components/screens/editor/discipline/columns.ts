import {GridColDef} from "@mui/x-data-grid";
import {IDisciplineCode, IDisciplineName, IDisciplineType} from "../../../../features/models";
import {ITerm} from "../../../../features/models";
import {ISpeciality} from "../../../../features/models";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    {
        field: 'name',
        headerName: 'Название',
        width: 120,
        renderCell: props => (props.value as IDisciplineName).name
    },
    {
        field: 'code',
        headerName: 'Код',
        width: 120,
        renderCell: props => (props.value as IDisciplineCode).code
    },
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
        renderCell: props => (props.value as ITerm).id
    },
    {
        field: 'speciality',
        headerName: 'Специальность',
        width: 140,
        renderCell: props => (props.value as ISpeciality).name
    }
]