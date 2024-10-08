import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {useDeleteDisciplineMutation, useGetDisciplinesQuery} from "../../../../store/apis";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {CreateDisciplineDialog} from "./dialogs";
import {useDialog} from "../../../../hooks";
import {UpdateDisciplineDialog} from "./dialogs";
import {useState} from "react";
import {IDiscipline} from "../../../../features/models";

export const AvailableDisciplinesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const {data} = useGetDisciplinesQuery(paginationQuery)
    const [selected, setSelected] = useState<IDiscipline>({} as IDiscipline)
    const [remove] = useDeleteDisciplineMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (discipline: IDiscipline) => {
        setSelected(discipline)
        updateDialog.show()
    }

    const handleDelete = (discipline: IDiscipline) => remove(discipline.id)

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
            <CreateDisciplineDialog {...createDialog}/>
            <UpdateDisciplineDialog discipline={selected} {...updateDialog}/>
        </Container>
    )
}

