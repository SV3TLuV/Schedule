import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {useGetClassroomsQuery, useRestoreClassroomMutation} from "../../../../store/apis";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {columns} from "./columns.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {IClassroom} from "../../../../features/models";

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