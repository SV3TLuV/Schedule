import {GridColDef} from "@mui/x-data-grid";
import {IClassroomType} from "../../../features/models/IClassroomType.ts";

export const columns: GridColDef[] = [
    { field: 'id', headerName: 'Id' },
    { field: 'cabinet', headerName: 'Кабинет', width: 120 },
    {
        field: 'types',
        headerName: 'Виды',
        width: 180,
        renderCell: props => {
            return (props.value as IClassroomType[])
                .map(classroom => classroom.name)
                .join(", ")
        }
    }
]