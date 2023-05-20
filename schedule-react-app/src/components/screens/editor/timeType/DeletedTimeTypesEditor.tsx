import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetTimeTypesQuery, useRestoreTimeTypeMutation} from "../../../../store/apis/timeTypeApi.ts";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {ITimeType} from "../../../../features/models/ITimeType.ts";

export const DeletedTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Deleted)
    const {data} = useGetTimeTypesQuery(paginationQuery)
    const [restore] = useRestoreTimeTypeMutation()

    const handleRestore = (timeType: ITimeType) => restore(timeType.id)

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