import {
    DataGrid,
    DataGridProps,
    GridColDef,
} from "@mui/x-data-grid";
import {IPagedList} from "../features/models/IPagedList";
import {Loading} from "./Loading";
import {IPaginatedQuery} from "../features/queries/IPaginatedQuery";
import {Pagination, PaginationItem} from "@mui/lab";


interface IBaseDataGridProps<T> {
    columns: GridColDef[]
    list: IPagedList<T> | undefined
    paginationModel: IPaginatedQuery
    onPaginationModelChange: (model: IPaginatedQuery) => void
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

    if (!list) {
        return (
            <Loading/>
        )
    }

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
            columns={columns}
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