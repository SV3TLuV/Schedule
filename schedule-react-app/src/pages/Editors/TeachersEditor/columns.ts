import {GridColDef} from "@mui/x-data-grid";
import {IGroup} from "../../../features/models/IGroup.ts";
import {IDiscipline} from "../../../features/models/IDiscipline.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'surname', headerName: 'Фамилия', width: 120 },
    { field: 'name', headerName: 'Имя', width: 120 },
    { field: 'middleName', headerName: 'Отчество', width: 120 },
    { field: 'email', headerName: 'Почта', width: 280 },
    {
        field: 'groups',
        headerName: 'Группы',
        width: 120,
        renderCell: props => (props.value as IGroup[])
            .map(group => group.name)
            .join(',')
    },
    {
        field: 'disciplines',
        headerName: 'Дисциплины',
        width: 140,
        renderCell: props => (props.value as IDiscipline[])
            .map(discipline => discipline.name)
            .join(',')
    }
]