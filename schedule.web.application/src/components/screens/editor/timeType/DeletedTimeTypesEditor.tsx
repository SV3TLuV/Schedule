import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {useGetTimeTypesQuery, useRestoreTimeTypeMutation} from "../../../../store/apis";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {ITimeType} from "../../../../features/models";

export const DeletedTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
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