import {usePaginationQuery} from "../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../common/enums/QueryFilter.ts";
import {useGetSpecialitiesQuery} from "../../../redux/apis/specialityApi.ts";
import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {DataGridWithPagination} from "../../DataGridWithPagination/DataGridWithPagination.tsx";
import {EditorToolbar} from "../EditorToolbar.tsx";

export const DeletedSpecialitiesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Deleted)
    const {data} = useGetSpecialitiesQuery(paginationQuery)

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