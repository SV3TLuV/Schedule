import {GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {useGetGroupsQuery} from "../../services/groupApi";
import {useCallback} from "react";
import {PaginationDataGrid} from "../PaginationDataGrid";
import {usePaginationQuery} from "../../hooks/usePaginationQuery";


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
        type: "number",
    },
    {
        field: "course",
        headerName: "Курс",
        width: 80,
        type: "number",
        renderCell: params => (params.value as ICourse).value,
    },
    {
        field: "specialityCode",
        headerName: "Код специальности",
        width: 180,
        type: "string",
        renderCell: params => (params.value as ISpecialityCode).code,
    },
    {
        field: "change",
        headerName: "Изменить",
        sortable: false,
        width: 120,
        renderCell: params => (
            <Button>
                Изменить
            </Button>
        ),
    },
    {
        field: "delete",
        headerName: "Удалить",
        sortable: false,
        width: 120,
        renderCell: params => (
            <Button>
                Удалить
            </Button>
        ),
    },
]

interface IGroupEditorProps {
    filter: QueryFilter
}

export const GroupEditor = ({ filter }: IGroupEditorProps) => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(filter)
    const {data: list} = useGetGroupsQuery(paginationQuery)

    const toolbar = useCallback(() => (
        <GridToolbarContainer style={{ padding: "20px" }}>
            <Button>
                Добавить
            </Button>
        </GridToolbarContainer>
    ), [])

    return (
        <PaginationDataGrid
            columns={columns}
            list={list}
            components={{ Toolbar: toolbar }}
            paginationModel={paginationQuery}
            onPaginationModelChange={setPaginationQuery}
        />
    )
}