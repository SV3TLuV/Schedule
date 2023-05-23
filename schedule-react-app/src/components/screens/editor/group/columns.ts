import {GridColDef} from "@mui/x-data-grid";
import {ITerm} from "../../../../features/models/ITerm";
import {IGroup} from "../../../../features/models/IGroup";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 100 },
    { field: 'enrollmentYear', headerName: 'Год поступления', width: 160 },
    {
        field: 'term',
        headerName: 'Семестр',
        width: 120,
        renderCell: props => (props.value as ITerm).id
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