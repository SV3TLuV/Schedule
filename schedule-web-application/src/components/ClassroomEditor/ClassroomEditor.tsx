import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useGetClassroomsQuery} from "../../services/classroomApi";
import {useCallback} from "react";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {IClassroomType} from "../../features/models/IClassroomType";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40 },
    { field: "cabinet", headerName: "Кабинет", width: 120 },
    {
        field: "types",
        headerName: "Виды",
        width: 200,
        renderCell: params =>
            (params.value as IClassroomType[])
                .map(type => type.name)
                .join(", ")
    },
]

interface IClassroomEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const ClassroomEditor = ({filter, type}: IClassroomEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetClassroomsQuery(paginationQuery)

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