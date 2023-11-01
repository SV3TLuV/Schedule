import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {columns} from "./columns.ts";
import {useGetDisciplineNamesQuery, useRestoreDisciplineNameMutation} from "../../../../store/apis";
import {IDisciplineName} from "../../../../features/models";

export const DeletedDisciplineNamesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
    const {data} = useGetDisciplineNamesQuery(paginationQuery)
    const [restore] = useRestoreDisciplineNameMutation()

    const handleRestore = (disciplineName: IDisciplineName) => restore(disciplineName.id)

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