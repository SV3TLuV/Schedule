import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {useGetTeachersQuery} from "../../services/teacherApi";
import {useCallback} from "react";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", hideable: true },
    { field: "surname", headerName: "Фамилия", width: 120 },
    { field: "name", headerName: "Имя", width: 120 },
    { field: "middleName", headerName: "Отчество", width: 120 },
    { field: "email", headerName: "Почта", width: 240 }
]

interface ITeacherEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const TeacherEditor = ({filter, type}: ITeacherEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetTeachersQuery(paginationQuery)

    return (
        <PaginationDataGrid
            columns={columns}
            list={list}
            type={type}
            paginationModel={paginationQuery}
            onPaginationModelChange={setPaginationQuery}
        />
    )
}