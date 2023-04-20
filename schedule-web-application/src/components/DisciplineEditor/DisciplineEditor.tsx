import {QueryFilter} from "../../common/enums/QueryFilter";
import {useCallback} from "react";
import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useGetDisciplinesQuery} from "../../services/disciplineApi";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {ITerm} from "../../features/models/ITerm";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40 },
    { field: "name", headerName: "Название", width: 160 },
    { field: "code", headerName: "Код", width: 100 },
    { field: "totalHours", headerName: "Количество часов", width: 160 },
    {
        field: "specialityCode",
        headerName: "Код специальности",
        width: 180,
        renderCell: params => (params.value as ISpecialityCode).code
    },
    {
        field: "term",
        headerName: "Семестр",
        width: 100,
        renderCell: params => (params.value as ITerm).value
    },
]

interface IDisciplineEditorProps {
    filter: QueryFilter
    type: PaginationDataGridType
}

export const DisciplineEditor = ({ filter, type }: IDisciplineEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetDisciplinesQuery(paginationQuery)

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