import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetGroupsQuery} from "../../../../store/apis/groupApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";

export const AvailableGroupsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetGroupsQuery(paginationQuery)

    const toolbar = <EditorToolbar
        paginationQuery={paginationQuery}
        setPaginationQuery={setPaginationQuery}
        onCreate={() => console.log('a')}
    />

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => toolbar}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onUpdate={() => console.log('a')}
                onDelete={() => console.log('a')}
            />
        </Container>
    )
}

