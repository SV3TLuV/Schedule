import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {useDeleteTimeTypeMutation, useGetTimeTypesQuery} from "../../../../store/apis";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {useDialog} from "../../../../hooks";
import {useState} from "react";
import {ITimeType} from "../../../../features/models";
import {CreateTimeTypeDialog} from "./dialogs";
import {UpdateTimeTypeDialog} from "./dialogs";

export const AvailableTimeTypesEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<ITimeType>({} as ITimeType)
    const {data} = useGetTimeTypesQuery(paginationQuery)
    const [remove] = useDeleteTimeTypeMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (timeType: ITimeType) => {
        setSelected(timeType)
        updateDialog.show()
    }

    const handleDelete = (timeType: ITimeType) => remove(timeType.id)

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
            <CreateTimeTypeDialog {...createDialog}/>
            <UpdateTimeTypeDialog timeType={selected} {...updateDialog}/>
        </Container>
    )
}

