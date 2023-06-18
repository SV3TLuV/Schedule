import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {useGetTimesQuery, useRestoreTimeMutation} from "../../../../store/apis";
import {DataGridWithPagination} from "../../../ui";
import {ITime} from "../../../../features/models";
import {EditorToolbar} from "../EditorToolbar.tsx";

export const DeletedTimesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
    const {data} = useGetTimesQuery(paginationQuery)
    const [restore] = useRestoreTimeMutation()

    const handleRestore = (time: ITime) => restore(time.id)

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