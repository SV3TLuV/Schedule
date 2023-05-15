import {GridColDef} from "@mui/x-data-grid";
import {IDiscipline} from "../../../features/models/IDiscipline.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 120 },
    { field: 'code', headerName: 'Код', width: 120 },
    { field: 'maxTermId', headerName: 'Кол-во семестров', width: 160 },
    {
        field: 'disciplines',
        headerName: 'Дисциплины',
        width: 160,
        renderCell: props => (props.value as IDiscipline[])
            .map(discipline => discipline.name)
            .join(',')
    }
]