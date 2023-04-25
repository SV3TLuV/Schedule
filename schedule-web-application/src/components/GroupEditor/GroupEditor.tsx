import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpeciality} from "../../features/models/ISpeciality";
import {Button} from "react-bootstrap";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {useGetGroupsQuery} from "../../services/groupApi";
import {useCallback, useMemo} from "react";
import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {IGroup} from "../../features/models/IGroup";


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
    {
        field: "enrollmentYear",
        headerName: "Год поступления",
        width: 160,
        type: "string",
    },
    {
        field: "course",
        headerName: "Курс",
        width: 80,
        type: "number",
        renderCell: params => (params.value as ICourse).value,
    },
    {
        field: "speciality",
        headerName: "Код специальности",
        width: 180,
        type: "string",
        renderCell: params => (params.value as ISpeciality).code,
    },
    {
        field: "mergedGroups",
        headerName: "Учатся с",
        width: 200,
        type: "string",
        renderCell: params =>
            (params.value as IGroup[])
                .map(g => g.name)
                .join(", "),
    },
]

interface IGroupEditorProps {
    filter: QueryFilter,
    type: PaginationDataGridType
}

export const GroupEditor = ({ filter, type }: IGroupEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetGroupsQuery(paginationQuery)

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