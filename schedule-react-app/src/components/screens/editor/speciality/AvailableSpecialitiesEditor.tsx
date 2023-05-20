import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useDeleteSpecialityMutation, useGetSpecialitiesQuery} from "../../../../store/apis/specialityApi.ts";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui/DataGridWithPagination.tsx";
import {useDialog} from "../../../../hooks/useDialog";
import {useState} from "react";
import {ISpeciality} from "../../../../features/models/ISpeciality";
import {CreateSpecialityDialog} from "./Dialogs/CreateSpecialityDialog";
import {UpdateSpecialityDialog} from "./Dialogs/UpdateSpecialityDialog";

export const AvailableSpecialitiesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const [selected, setSelected] = useState<ISpeciality>({} as ISpeciality)
    const {data} = useGetSpecialitiesQuery(paginationQuery)
    const [remove] = useDeleteSpecialityMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (speciality: ISpeciality) => {
        setSelected(speciality)
        updateDialog.show()
    }

    const handleDelete = (speciality: ISpeciality) => remove(speciality.id)

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
            <CreateSpecialityDialog {...createDialog}/>
            <UpdateSpecialityDialog speciality={selected} {...updateDialog}/>
        </Container>
    )
}