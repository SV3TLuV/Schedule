import {usePaginationQuery} from "../../../../hooks";
import {QueryFilter} from "../../../../common/enums";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {columns} from "./columns.ts";
import {useGetDisciplineCodesQuery, useRestoreDisciplineCodeMutation} from "../../../../store/apis";
import {IDisciplineCode} from "../../../../features/models";

export const DeletedDisciplineCodesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery({
        filter: QueryFilter.Deleted
    })
    const {data} = useGetDisciplineCodesQuery(paginationQuery)
    const [restore] = useRestoreDisciplineCodeMutation()

    const handleRestore = (disciplineCode: IDisciplineCode) => restore(disciplineCode.id)

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