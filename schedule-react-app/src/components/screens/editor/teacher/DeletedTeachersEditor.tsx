import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {useGetTeachersQuery, useRestoreTeacherMutation} from "../../../../store/apis/teacherApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {ITeacher} from "../../../../features/models/ITeacher.ts";

export const DeletedTeachersEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data} = useGetTeachersQuery(paginationQuery)
    const [restore] = useRestoreTeacherMutation()

    const handleRestore = (teacher: ITeacher) => restore(teacher.id)

    return (
        <Container style={{height: 'calc(100vh - 114px)', padding: 0}}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => (
                    <EditorToolbar
                        paginationQuery={paginationQuery}
                        setPaginationQuery={setPaginationQuery}
                    />
                )}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onRestore={handleRestore}
            />
        </Container>
    )
}