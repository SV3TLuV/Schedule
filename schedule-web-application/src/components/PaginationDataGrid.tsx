import {
    DataGrid,
    DataGridProps,
    GridColDef,
} from "@mui/x-data-grid";
import {IPagedList} from "../features/models/IPagedList";
import {Loading} from "./Loading";
import {IPaginatedQuery} from "../features/queries/IPaginatedQuery";
import {Pagination, PaginationItem} from "@mui/lab";
import {useMemo} from "react";
import {Button} from "react-bootstrap";

export type PaginationDataGridType =
    "available" |"deleted" | null;

export interface IPaginationDataGridProps<T> {
    columns: GridColDef[]
    list: IPagedList<T> | undefined
    paginationModel: IPaginatedQuery
    onPaginationModelChange: (model: IPaginatedQuery) => void
    components?: DataGridProps['components']
    type?: PaginationDataGridType
}

export const PaginationDataGrid = <T extends { id: number }>(
    {
        columns,
        list,
        paginationModel,
        onPaginationModelChange,
        components,
        type
    }: IPaginationDataGridProps<T>) => {

    if (!list) {
        return (
            <Loading/>
        )
    }

    const cols = useMemo(() => {
        switch (type) {
            case "available":
                return [
                    ...columns,
                    {
                        field: "change-button",
                        headerName: "Изменить",
                        sortable: false,
                        width: 120,
                        renderCell: () => (
                            <Button>
                                Изменить
                            </Button>
                        ),
                    },
                    {
                        field: "delete-button",
                        headerName: "Удалить",
                        sortable: false,
                        width: 120,
                        renderCell: () => (
                            <Button>
                                Удалить
                            </Button>
                        ),
                    }
                ]
            case "deleted":
                return [
                    ...columns,
                    {
                        field: "restore-button",
                        headerName: "Восстановить",
                        sortable: false,
                        width: 160,
                        renderCell: () => (
                            <Button>
                                Восстановить
                            </Button>
                        ),
                    }
                ]
            default:
                return columns
        }
    }, [columns, type])

    const pagination = () => {
        return (
            <Pagination
                color="primary"
                variant="outlined"
                shape="rounded"
                page={paginationModel.page}
                count={list.totalPages}
                // @ts-expect-error
                renderItem={(props2) => <PaginationItem {...props2} disableRipple />}
                onChange={(event: React.ChangeEvent<unknown>, value: number) => {
                    onPaginationModelChange({...paginationModel, page: value})
                }}
            />
        )
    }

    return (
        <DataGrid
            columns={cols}
            rows={list.items}
            rowCount={list.totalCount}
            initialState={{
                columns: {
                    columnVisibilityModel: {
                        id: false,
                    },
                },
            }}
            slots={{ pagination: pagination }}
            components={components}
            paginationMode="server"
            paginationModel={{
                page: paginationModel.page - 1,
                pageSize: paginationModel.pageSize,
            }}
            onPaginationModelChange={onPaginationModelChange}
            disableColumnMenu
            disableRowSelectionOnClick
        />
    )
}