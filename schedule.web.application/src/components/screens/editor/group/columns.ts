import {GridColDef} from "@mui/x-data-grid";
import {ITerm} from "../../../../features/models";
import {IGroup} from "../../../../features/models";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 100 },
    { field: 'enrollmentYear', headerName: 'Год поступления', width: 160 },
    {
        field: 'term',
        headerName: 'Курс | Семестр',
        width: 150,
        renderCell: props => {
            const term = props.value as ITerm
            return `Курс: ${term.course.id} | Семестр: ${term.id}`
        }
    },
    {
        field: 'mergedGroups',
        headerName: 'Обучается с',
        width: 200,
        renderCell: props => (props.value as IGroup[])
            .map(group => group.name)
            .join(',')
    }
]