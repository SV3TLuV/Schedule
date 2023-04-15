import {DataGrid, DataGridProps, GridColDef} from "@mui/x-data-grid";
import {useEffect, useState} from "react";
import {IPagedList} from "../features/models/IPagedList";
import {Loading} from "./Loading";
import {IPaginationModel} from "../features/models/IPaginationModel";


interface IBaseDataGridProps<T> {
    columns: GridColDef[]
    list: IPagedList<T> | undefined
    paginationModel: IPaginationModel
    onPaginationModelChange: (model: IPaginationModel) => void
    components?: DataGridProps['components']
}

export const PaginationDataGrid = <T extends { id: number }>(
    {
        columns,
        list,
        paginationModel,
        onPaginationModelChange,
        components
    }: IBaseDataGridProps<T>) => {

    const [items, setItems] = useState<T[]>([])

    useEffect(() => {
        if (list) {
            const itemIds = items.map(item => item.id)
            const newItems = list.items.filter(item => !itemIds.includes(item.id))
            setItems(value => [...value, ...newItems])
        }
    }, [list])

    if (!list) {
        return (
            <Loading/>
        )
    }

    return (
        <DataGrid
            columns={columns}
            rows={items}
            rowCount={list.totalCount}
            components={components}
            paginationModel={paginationModel}
            onPaginationModelChange={onPaginationModelChange}
            disableColumnMenu
            disableRowSelectionOnClick
        />
    )
}