import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";
import {IClassroom} from "../../../../features/models/IClassroom.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {CreateClassroomDialog} from "./dialogs/CreateClassroomDialog.tsx";
import {UpdateClassroomDialog} from "./dialogs/UpdateClassroomDialog.tsx";
import {IEditor} from "../IEditor.tsx";
import {useDialog} from "../../../../hooks/useDialog.ts";
import {useState} from "react";

export const ClassroomEditor = (
    {
        data,
        onCreate,
        onUpdate,
        onDelete,
        onRestore,
        paginationQuery,
        setPaginationQuery,
    }: IEditor<IClassroom>) => {
    const [selected, setSelected] = useState<IClassroom>({} as IClassroom)
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleCreate = (classroom: IClassroom) => setSelected(classroom)
    const handleUpdate = (classroom: IClassroom) => setSelected(classroom)

    const toolbar = <EditorToolbar
        paginationQuery={paginationQuery}
        setPaginationQuery={setPaginationQuery}
        onCreate={onCreate}
    />

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => toolbar}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onUpdate={onUpdate}
                onDelete={onDelete}
                onRestore={onRestore}
            />
            <CreateClassroomDialog {...createDialog}/>
            <UpdateClassroomDialog classroom={selected} {...updateDialog}/>
        </Container>
    )
}