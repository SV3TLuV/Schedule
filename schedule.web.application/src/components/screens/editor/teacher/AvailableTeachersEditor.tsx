import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {
    useDeleteTeacherMutation, useExportTeacherMutation,
    useGetTeachersQuery,
    useImportTeacherMutation
} from "../../../../store/apis";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui";
import {useDialog} from "../../../../hooks";
import {useState} from "react";
import {ITeacher} from "../../../../features/models";
import {CreateTeacherDialog} from "./dialogs";
import {UpdateTeacherDialog} from "./dialogs";
import {convertFileToBlob} from "../../../../utils/convertFileToBlob.ts";
import {SelectFileDialog} from "../../../ui/SelectFileDialog.tsx";

export const AvailableTeachersEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<ITeacher>({} as ITeacher)
    const {data} = useGetTeachersQuery(paginationQuery)
    const [remove] = useDeleteTeacherMutation()
    const [importTeacher] = useImportTeacherMutation()
    const [exportTeacher] = useExportTeacherMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()
    const selectFileDialog = useDialog()

    const handleUpdate = (teacher: ITeacher) => {
        setSelected(teacher)
        updateDialog.show()
    }

    const handleDelete = (teacher: ITeacher) => remove(teacher.id)

    const handleImport = async (file: File) => {
        const blob = await convertFileToBlob(file)
        await importTeacher(blob)
    }

    const handleExport = async () => await exportTeacher()

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
                        onImport={selectFileDialog.show}
                        onExport={handleExport}
                    />
                )}
                paginationModel={paginationQuery}
                onPaginationModelChange={setPaginationQuery}
                onUpdate={handleUpdate}
                onDelete={handleDelete}
            />
            <CreateTeacherDialog {...createDialog}/>
            <UpdateTeacherDialog teacher={selected} {...updateDialog}/>
            <SelectFileDialog
                show={selectFileDialog.open}
                onClose={selectFileDialog.close}
                onSelect={handleImport}
            />
        </Container>
    )
}
