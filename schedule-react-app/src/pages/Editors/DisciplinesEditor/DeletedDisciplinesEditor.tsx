import {usePaginationQuery} from "../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../common/enums/QueryFilter.ts";
import {useGetDisciplinesQuery} from "../../../redux/apis/disciplineApi.ts";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../components/DataGridWithPagination/DataGridWithPagination.tsx";
import {columns} from "./columns.ts";

export const DeletedDisciplinesEditor = () => {
    const [paginationModel, setPaginationModel] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetDisciplinesQuery(paginationModel)

    return (
        <Container style={{height: 'calc(100vh - 114px)', padding: 0}}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                paginationModel={paginationModel}
                onPaginationModelChange={setPaginationModel}
                onRestore={() => console.log('a')}
            />
        </Container>
    )
}