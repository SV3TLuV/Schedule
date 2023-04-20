import {PaginationDataGrid, PaginationDataGridType} from "../PaginationDataGrid";
import {GridColDef} from "@mui/x-data-grid";
import {IDiscipline} from "../../features/models/IDiscipline";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";
import {useGetSpecialityCodesQuery} from "../../services/specialityCodeApi";


const columns: GridColDef[] = [
    {
        field: "id",
        headerName: "ID",
        width: 40,
        type: "number",
    },
    {
        field: "code",
        headerName: "Код специальности",
        width: 160,
        type: "string",
    },
    {
        field: "name",
        headerName: "Название",
        width: 160,
        type: "string",
    },
    {
        field: "disciplines",
        headerName: "Дисциплины",
        width: 200,
        renderCell: params =>
            (params.value as IDiscipline[])
                .map(d => d.name)
                .join(", "),
    }
]

interface ISpecialityEditorProps {
    filter: QueryFilter,
    type: PaginationDataGridType
}

export const SpecialityEditor = ({ filter, type }: ISpecialityEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetSpecialityCodesQuery(paginationQuery)

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