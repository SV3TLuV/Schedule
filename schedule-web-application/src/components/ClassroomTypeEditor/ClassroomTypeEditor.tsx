import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {GridColDef  } from "@mui/x-data-grid";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {useGetClassroomTypesQuery} from "../../services/classroomTypeApi";
import {QueryFilter} from "../../common/enums/QueryFilter";


const columns: GridColDef[] = [
    {
        field: "id",
        headerName: "ID",
        width: 40,
        type: "number",
    },
    {
        field: "name",
        headerName: "Название",
        width: 160,
        type: "string",
    },
]

interface IClassroomTypeEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const ClassroomTypeEditor = ({filter, type}: IClassroomTypeEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetClassroomTypesQuery(paginationQuery)

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