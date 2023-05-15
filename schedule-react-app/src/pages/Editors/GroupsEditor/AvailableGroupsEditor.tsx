import {Container} from "react-bootstrap";
import {usePaginationQuery} from "../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../common/enums/QueryFilter.ts";
import {useGetGroupsQuery} from "../../../redux/apis/groupApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../components/DataGridWithPagination/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";

export const AvailableGroupsEditor = () => {
    const [paginationModel, setPaginationModel] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetGroupsQuery(paginationModel)

    const toolbar = <EditorToolbar onCreate={() => console.log('a')}/>

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => toolbar}
                paginationModel={paginationModel}
                onPaginationModelChange={setPaginationModel}
                onUpdate={() => console.log('a')}
                onDelete={() => console.log('a')}
            />
        </Container>
    )
}

