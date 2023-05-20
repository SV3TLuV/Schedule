import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";
import {IClassroom} from "../../../../features/models/IClassroom.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {IEditor} from "../Editor.ts";
import {CreateClassroomDialog} from "./dialogs/CreateClassroomDialog.tsx";
import {useDialog} from "../../../../hooks/useDialog.ts";
import {UpdateClassroomDialog} from "./dialogs/UpdateClassroomDialog.tsx";
import {useState} from "react";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useDeleteClassroomMutation} from "../../../../store/apis/classroomApi.ts";

export const ClassroomEditor = (
    {
        data,
        paginationQuery,
        setPaginationQuery,
    }: IEditor<IClassroom>) => {
    const [classroom, setClassroom] = useState<IClassroom>({} as IClassroom)
    const [remove] = useDeleteClassroomMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const onCreate = paginationQuery.filter === QueryFilter.Available
        ? createDialog.show
        : undefined

    const onUpdate = paginationQuery.filter === QueryFilter.Available
        ? (classroom: IClassroom) => {
            setClassroom(classroom)
            updateDialog.show()
        }
        : undefined

    const onDelete = paginationQuery.filter === QueryFilter.Available
        ? (classroom: IClassroom) => remove(classroom.id)
        : undefined

    const onRestore = paginationQuery.filter === QueryFilter.Deleted
        ? (classroom: IClassroom) => console.log(classroom)
        : undefined

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
            <UpdateClassroomDialog classroom={classroom} {...updateDialog}/>
        </Container>
    )
}