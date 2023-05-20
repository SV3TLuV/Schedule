import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useDeleteTimeMutation, useGetTimesQuery} from "../../../../store/apis/timeApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {useDialog} from "../../../../hooks/useDialog";
import {useState} from "react";
import {ITime} from "../../../../features/models/ITime";
import {CreateTimeDialog} from "./dialogs/CreateTimeDialog";
import {UpdateTimeDialog} from "./dialogs/UpdateTimeDialogs";

export const AvailableTimesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const [selected, setSelected] = useState<ITime>({} as ITime)
    const {data} = useGetTimesQuery(paginationQuery)
    const [remove] = useDeleteTimeMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (time: ITime) => {
        setSelected(time)
        updateDialog.show()
    }

    const handleDelete = (time: ITime) => remove(time.id)

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
            <CreateTimeDialog {...createDialog}/>
            <UpdateTimeDialog time={selected} {...updateDialog}/>
        </Container>
    )
}

