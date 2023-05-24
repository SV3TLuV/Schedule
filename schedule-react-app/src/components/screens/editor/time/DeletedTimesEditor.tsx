import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetTimesQuery, useRestoreTimeMutation} from "../../../../store/apis/timeApi.ts";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {ITime} from "../../../../features/models/ITime.ts";
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