import {DataGrid, GridColDef} from "@mui/x-data-grid";
import {IPagedList} from "../../features/models/IPagedList.ts";
import {IPaginatedQuery} from "../../features/queries/IPaginatedQuery.ts";
import {Pagination} from "antd";
import {Loading} from "../Loading/Loading.tsx";
import {Button} from "react-bootstrap";
import {JSXElementConstructor} from "react";

export interface IDataGridWithPaginationProps<T> {
    columns: GridColDef[]
    list: IPagedList<T> | undefined
    paginationModel: IPaginatedQuery
    onPaginationModelChange: (model: IPaginatedQuery) => void
    toolbar?: JSXElementConstructor<any>
    onUpdate?: (item: T) => void
    onDelete?: (item: T) => void
    onRestore?: (item: T) => void
}

export const DataGridWithPagination = <T extends { id: number }>(
    {
        columns,
        list,
        paginationModel,
        onPaginationModelChange,
        toolbar,
        onUpdate,
        onDelete,
        onRestore
    }: IDataGridWithPaginationProps<T>) => {

    if (!list) {
        return <Loading/>
    }

    const cols: GridColDef[] = (function () {
        const buttons: GridColDef[] = []

        if (onUpdate) {
            buttons.push({
                field: "update-btn",
                headerName: "Изменить",
                width: 120,
                renderCell: props => {
                    const handleClick = () => onUpdate(props.row as T)

                    return (
                        <Button
                            variant='outline-primary'
                            onClick={handleClick}
                        >
                            Изменить
                        </Button>
                    )
                }
            })
        }

        if (onRestore) {
            buttons.push({
                field: "restore-btn",
                headerName: "Восстановить",
                width: 150,
                renderCell: props => {
                    const handleClick = () => onRestore(props.row as T)

                    return (
                        <Button
                            variant='outline-primary'
                            onClick={handleClick}
                        >
                            Восстановить
                        </Button>
                    )
                }
            })
        }

        if (onDelete) {
            buttons.push({
                field: "delete-btn",
                headerName: "Удалить",
                width: 120,
                renderCell: props => {
                    const handleClick = () => onDelete(props.row as T)

                    return (
                        <Button
                            variant='outline-danger'
                            onClick={handleClick}
                        >
                            Удалить
                        </Button>
                    )
                }
            })
        }

        return columns.concat(buttons)
    })()

    const onPageChange = (page: number, pageSize: number) => {
        onPaginationModelChange({ page, pageSize })
    }

    const pagination = () => {
        return (
            <Pagination
                current={paginationModel.page}
                pageSize={paginationModel.pageSize}
                onChange={onPageChange}
                total={list.totalCount}
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
            slots={{
                pagination: pagination,
                toolbar: toolbar
            }}
            paginationMode='server'
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