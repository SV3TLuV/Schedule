import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetTimeTypesQuery} from "../../../../store/apis/timeTypeApi.ts";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";

export const AvailableTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetTimeTypesQuery(paginationQuery)

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

