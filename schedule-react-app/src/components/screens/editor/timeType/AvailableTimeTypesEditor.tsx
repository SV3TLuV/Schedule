import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useDeleteTimeTypeMutation, useGetTimeTypesQuery} from "../../../../store/apis/timeTypeApi.ts";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {useDialog} from "../../../../hooks/useDialog";
import {useState} from "react";
import {ITimeType} from "../../../../features/models/ITimeType";
import {CreateTimeTypeDialog} from "./dialogs/CreateTimeTypeDialog";
import {UpdateTimeTypeDialog} from "./dialogs/UpdateTimeTypeDialog";

export const AvailableTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const [selected, setSelected] = useState<ITimeType>({} as ITimeType)
    const {data} = useGetTimeTypesQuery(paginationQuery)
    const [remove] = useDeleteTimeTypeMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (timeType: ITimeType) => {
        setSelected(timeType)
        updateDialog.show()
    }

    const handleDelete = (timeType: ITimeType) => remove(timeType.id)

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => (
                    <EditorToolbar
                        paginationQuery={paginationQuery}
                        setPaginationQuery={setPaginationQuery}
                        onCreate={createDialog.show}
                    />
                )}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onUpdate={handleUpdate}
                onDelete={handleDelete}
            />
            <CreateTimeTypeDialog {...createDialog}/>
            <UpdateTimeTypeDialog timeType={selected} {...updateDialog}/>
        </Container>
    )
}

