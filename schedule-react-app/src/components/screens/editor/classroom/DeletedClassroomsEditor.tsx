import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetClassroomsQuery, useRestoreClassroomMutation} from "../../../../store/apis/classroomApi.ts";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {IClassroom} from "../../../../features/models/IClassroom.ts";

export const DeletedClassroomsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
    const {data} = useGetClassroomsQuery(paginationQuery)
    const [restore] = useRestoreClassroomMutation()

    const handleRestore = (classroom: IClassroom) => restore(classroom.id)

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
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