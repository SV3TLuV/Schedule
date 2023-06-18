import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {useDeleteTimeMutation, useGetTimesQuery} from "../../../../store/apis";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui";
import {useDialog} from "../../../../hooks";
import {useState} from "react";
import {ITime} from "../../../../features/models";
import {CreateTimeDialog} from "./dialogs";
import {UpdateTimeDialog} from "./dialogs";

export const AvailableTimesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
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

