import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../components/DataGridWithPagination/DataGridWithPagination.tsx";
import {useGetClassroomsQuery} from "../../../redux/apis/classroomApi.ts";
import {usePaginationQuery} from "../../../hooks/usePaginationQuery.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {QueryFilter} from "../../../common/enums/QueryFilter.ts";
import {columns} from "./columns.ts";

export const AvailableClassroomsEditor = () => {
    const [paginationModel, setPaginationModel] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetClassroomsQuery(paginationModel)

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