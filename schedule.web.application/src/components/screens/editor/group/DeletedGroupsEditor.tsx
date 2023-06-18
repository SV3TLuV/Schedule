import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {useGetGroupsQuery, useRestoreGroupMutation} from "../../../../store/apis";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui";
import {IGroup} from "../../../../features/models";

export const DeletedGroupsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
    const {data} = useGetGroupsQuery(paginationQuery)
    const [restore] = useRestoreGroupMutation()

    const handleRestore = (group: IGroup) => restore(group.id)

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