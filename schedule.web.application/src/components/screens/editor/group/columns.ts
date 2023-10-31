import {GridColDef} from "@mui/x-data-grid";
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
            const group = props.row as IGroup
            const course = group.isAfterEleven ? group.term.course.id - 1 : group.term.course.id
            const realTerm = group.isAfterEleven ? group.term.id - 2 : group.term.id
            return `Курс: ${course} | Семестр: ${realTerm}`
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