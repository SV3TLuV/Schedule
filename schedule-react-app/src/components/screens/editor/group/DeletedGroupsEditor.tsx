import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetGroupsQuery} from "../../../../store/apis/groupApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";

export const DeletedGroupsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Deleted)
    const {data} = useGetGroupsQuery(paginationQuery)

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