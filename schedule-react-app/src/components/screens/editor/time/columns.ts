import {GridColDef} from "@mui/x-data-grid";
import {ITimeType} from "../../../../features/models/ITimeType.ts";

function getHourWord(number: number): string {
    let word: string;
    const lastDigit = number % 10;
    const lastTwoDigits = number % 100;

    if (lastTwoDigits >= 11 && lastTwoDigits <= 19) {
        word = 'часов';
    } else if (lastDigit === 1) {
        word = 'час';
    } else if (lastDigit >= 2 && lastDigit <= 4) {
        word = 'часа';
    } else {
        word = 'часов';
    }

    return `${number} ${word}`;
}

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'start', headerName: 'Начало', width: 120 },
    { field: 'end', headerName: 'Конец', width: 120 },
    { field: 'lessonNumber', headerName: 'Номер пары', width: 140 },
    {
        field: 'duration',
        headerName: 'Длительность',
        width: 140,
        renderCell: props => getHourWord(props.value as number)
    },
    {
        field: 'type',
        headerName: 'Вид',
        width: 120,
        renderCell: props => (props.value as ITimeType).name
    }
]