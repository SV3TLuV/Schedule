import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetTimeTypesQuery} from "../../../../store/apis/timeTypeApi.ts";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";

export const DeletedTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Deleted)
    const {data} = useGetTimeTypesQuery(paginationQuery)

    const toolbar = <EditorToolbar
        paginationQuery={paginationQuery}
        setPaginationQuery={setPaginationQuery}
    />

    return (
        <Container style={{height: 'calc(100vh - 114px)', padding: 0}}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => toolbar}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onRestore={() => console.log('a')}
            />
        </Container>
    )
}