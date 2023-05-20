import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useDeleteTeacherMutation, useGetTeachersQuery} from "../../../../store/apis/teacherApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {useDialog} from "../../../../hooks/useDialog";
import {useState} from "react";
import {ITeacher} from "../../../../features/models/ITeacher";
import {CreateTeacherDialog} from "./dialogs/CreateTeacherDialog";
import {UpdateTeacherDialog} from "./dialogs/UpdateTeacherDialog";

export const AvailableTeachersEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const [selected, setSelected] = useState<ITeacher>({} as ITeacher)
    const {data} = useGetTeachersQuery(paginationQuery)
    const [remove] = useDeleteTeacherMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (teacher: ITeacher) => {
        setSelected(teacher)
        updateDialog.show()
    }

    const handleDelete = (teacher: ITeacher) => remove(teacher.id)

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
            <CreateTeacherDialog {...createDialog}/>
            <UpdateTeacherDialog teacher={selected} {...updateDialog}/>
        </Container>
    )
}
