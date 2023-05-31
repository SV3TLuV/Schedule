import {GridColDef} from "@mui/x-data-grid";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 120 },
    { field: 'code', headerName: 'Код', width: 120 },
    { field: 'maxTermId', headerName: 'Кол-во семестров', width: 160 },
]