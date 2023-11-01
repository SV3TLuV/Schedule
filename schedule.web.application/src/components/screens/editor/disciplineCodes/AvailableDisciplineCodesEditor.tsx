import {useDialog, usePaginationQuery} from "../../../../hooks";
import {
    useDeleteDisciplineCodeMutation,
    useGetDisciplineCodesQuery,
} from "../../../../store/apis";
import {useState} from "react";
import {IDisciplineCode} from "../../../../features/models";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {CreateDisciplineCodeDialog, UpdateDisciplineCodeDialog} from "./dialogs";
import {columns} from "./columns.ts";

export const AvailableDisciplineCodesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data} = useGetDisciplineCodesQuery(paginationQuery)
    const [selected, setSelected] = useState<IDisciplineCode>({} as IDisciplineCode)
    const [remove] = useDeleteDisciplineCodeMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (disciplineCode: IDisciplineCode) => {
        setSelected(disciplineCode)
        updateDialog.show()
    }

    const handleDelete = (disciplineCode: IDisciplineCode) => remove(disciplineCode.id)

    return (
        <Container style={{ height: 'calc(100vh - 114px)', padding: 0 }}>
            <DataGridWithPagination
                columns={columns}
                list={data}
                toolbar={() => (
                    <EditorToolbar
                        paginationQuery={paginationQuery}
                        setPaginationQuery={setPaginationQuery}
                        onCreate={createDialog.show}
                    />
                )}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onUpdate={handleUpdate}
                onDelete={handleDelete}
            />
            <CreateDisciplineCodeDialog {...createDialog}/>
            <UpdateDisciplineCodeDialog disciplineCode={selected} {...updateDialog}/>
        </Container>
    )
}