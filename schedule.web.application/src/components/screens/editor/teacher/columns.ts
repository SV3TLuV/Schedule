import {GridColDef} from "@mui/x-data-grid";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'surname', headerName: 'Фамилия', width: 120 },
    { field: 'name', headerName: 'Имя', width: 120 },
    { field: 'middleName', headerName: 'Отчество', width: 120 },
    { field: 'email', headerName: 'Почта', width: 280 },
]