import {useDialog, usePaginationQuery} from "../../../../hooks";
import {useState} from "react";
import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {IDisciplineName} from "../../../../features/models";
import {CreateDisciplineNameDialog, UpdateDisciplineNameDialog} from "./dialogs";
import {useDeleteDisciplineNameMutation, useGetDisciplineNamesQuery} from "../../../../store/apis";
import {columns} from "./columns.ts";

export const AvailableDisciplineNamesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data} = useGetDisciplineNamesQuery(paginationQuery)
    const [selected, setSelected] = useState<IDisciplineName>({} as IDisciplineName)
    const [remove] = useDeleteDisciplineNameMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (disciplineName: IDisciplineName) => {
        setSelected(disciplineName)
        updateDialog.show()
    }

    const handleDelete = (disciplineName: IDisciplineName) => remove(disciplineName.id)

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
            <CreateDisciplineNameDialog {...createDialog}/>
            <UpdateDisciplineNameDialog disciplineName={selected} {...updateDialog}/>
        </Container>
    )
}