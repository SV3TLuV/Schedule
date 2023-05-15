import {GridColDef} from "@mui/x-data-grid";
import {ITerm} from "../../../features/models/ITerm.ts";
import {IGroup} from "../../../features/models/IGroup.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'name', headerName: 'Название', width: 80 },
    { field: 'enrollmentYear', headerName: 'Год поступления', width: 160 },
    {
        field: 'term',
        headerName: 'Семестр',
        width: 120,
        renderCell: props => (props.value as ITerm).value
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