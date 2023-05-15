import {GridColDef} from "@mui/x-data-grid";
import {ITimeType} from "../../../features/models/ITimeType.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'start', headerName: 'Начало', width: 120 },
    { field: 'end', headerName: 'Конец', width: 120 },
    { field: 'lessonNumber', headerName: 'Номер пары', width: 140 },
    {
        field: 'duration',
        headerName: 'Длительность',
        width: 140,
        renderCell: props => {
            const duration = props.value as number
            return (duration & 1) == 0
                ? `${duration} часа`
                : `${duration} час`
        }
    },
    {
        field: 'type',
        headerName: 'Вид',
        width: 120,
        renderCell: props => (props.value as ITimeType).name
    }
]