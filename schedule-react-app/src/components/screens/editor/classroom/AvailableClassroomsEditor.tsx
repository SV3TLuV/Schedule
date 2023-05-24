import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {
    useDeleteClassroomMutation,
    useGetClassroomsQuery,
} from "../../../../store/apis/classroomApi.ts";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";
import {CreateClassroomDialog} from "./dialogs/CreateClassroomDialog.tsx";
import {UpdateClassroomDialog} from "./dialogs/UpdateClassroomDialog.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {useState} from "react";
import {IClassroom} from "../../../../features/models/IClassroom.ts";
import {useDialog} from "../../../../hooks/useDialog.ts";

export const AvailableClassroomsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<IClassroom>({} as IClassroom)
    const {data} = useGetClassroomsQuery(paginationQuery)
    const [remove] = useDeleteClassroomMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (classroom: IClassroom) => {
        setSelected(classroom)
        updateDialog.show()
    }

    const handleDelete = (classroom: IClassroom) => remove(classroom.id)

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
            <CreateClassroomDialog {...createDialog}/>
            <UpdateClassroomDialog classroom={selected} {...updateDialog}/>
        </Container>
    )
}