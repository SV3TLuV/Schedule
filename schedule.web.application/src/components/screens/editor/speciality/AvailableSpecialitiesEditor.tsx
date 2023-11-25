import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {
    useDeleteSpecialityMutation, useExportSpecialityMutation,
    useGetSpecialitiesQuery,
    useImportSpecialityMutation
} from "../../../../store/apis";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui";
import {useDialog} from "../../../../hooks";
import {useState} from "react";
import {ISpeciality} from "../../../../features/models";
import {CreateSpecialityDialog} from "./Dialogs";
import {UpdateSpecialityDialog} from "./Dialogs";
import {SelectFileDialog} from "../../../ui/SelectFileDialog.tsx";
import {convertFileToBlob} from "../../../../utils/convertFileToBlob.ts";

export const AvailableSpecialitiesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<ISpeciality>({} as ISpeciality)
    const {data} = useGetSpecialitiesQuery(paginationQuery)
    const [remove] = useDeleteSpecialityMutation()
    const [importSpeciality] = useImportSpecialityMutation()
    const [exportSpeciality] = useExportSpecialityMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()
    const selectFileDialog = useDialog()

    const handleUpdate = (speciality: ISpeciality) => {
        setSelected(speciality)
        updateDialog.show()
    }

    const handleDelete = (speciality: ISpeciality) => remove(speciality.id)

    const handleImport = async (file: File) => {
        const blob = await convertFileToBlob(file)
        await importSpeciality(blob)
    }

    const handleExport = async () => await exportSpeciality()

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
            <CreateSpecialityDialog {...createDialog}/>
            <UpdateSpecialityDialog speciality={selected} {...updateDialog}/>
            <SelectFileDialog
                show={selectFileDialog.open}
                onClose={selectFileDialog.close}
                onSelect={handleImport}
            />
        </Container>
    )
}