import {DataGrid, GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {ICourse} from "../../features/models/ICourse";
import {ISpecialityCode} from "../../features/models/ISpecialityCode";
import {Button} from "react-bootstrap";
import {QueryFilter} from "../../common/enums/QueryFilter";
import {useGetGroupsQuery, useLazyGetGroupsQuery} from "../../services/groupApi";
import {useEffect, useMemo, useState} from "react";
import {data} from "autoprefixer";
import {IGroup} from "../../features/models/IGroup";
import {PaginationDataGrid} from "../PaginationDataGrid";
import {useDefaultPaginationModel} from "../../hooks/useDefaultPaginationModel";


const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 40, hideable: true},
    { field: "name", headerName: "Название", width: 160 },
    { field: "enrollmentYear", headerName: "Год поступления", width: 160 },
    {
        field: "course",
        headerName: "Курс",
        width: 80,
        renderCell: params => (params.value as ICourse).value
    },
    {
        field: "specialityCode",
        headerName: "Код специальности",
        width: 180,
        renderCell: params => (params.value as ISpecialityCode).code
    },
    {
        field: "change",
        headerName: "Изменить",
        width: 120,
        renderCell: params => (
            <Button>
                Изменить
            </Button>
        )
    },
    {
        field: "delete",
        headerName: "Удалить",
        width: 120,
        renderCell: params => (
            <Button>
                Удалить
            </Button>
        )
    }
]

interface IGroupEditorProps {
    filter: QueryFilter
}

export const GroupEditor = ({ filter }: IGroupEditorProps) => {
    const [paginationModel, setPaginationModel] = useDefaultPaginationModel()
    const {data: list, refetch} = useGetGroupsQuery({
        filter: filter,
        page: paginationModel.page + 1,
        count: paginationModel.pageSize
    })

    useEffect(() => {
        refetch()
    }, [paginationModel])

    const toolbar = useMemo(() => (
        <GridToolbarContainer style={{ padding: "20px" }}>
            <Button>
                Добавить
            </Button>
        </GridToolbarContainer>
    ), [])

    return (
        <div className="GroupEditor" style={{ height: "100vh" }}>
            <PaginationDataGrid
                columns={columns}
                list={list}
                components={{ Toolbar: () => toolbar }}
                paginationModel={paginationModel}
                onPaginationModelChange={setPaginationModel}
            />
        </div>
    )
}