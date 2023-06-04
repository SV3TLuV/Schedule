import {Container} from "react-bootstrap";
import {columns} from "./columns.ts";
import {usePaginationQuery} from "../../../../hooks";
import {useDeleteGroupMutation, useGetGroupsQuery} from "../../../../store/apis";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {DataGridWithPagination} from "../../../ui";
import {CreateGroupDialog} from "./dialogs";
import {useDialog} from "../../../../hooks";
import {useState} from "react";
import {IGroup} from "../../../../features/models";
import {UpdateGroupDialog} from "./dialogs";

export const AvailableGroupsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<IGroup>({} as IGroup)
    const {data} = useGetGroupsQuery(paginationQuery)
    const [remove] = useDeleteGroupMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (group: IGroup) => {
        setSelected(group)
        updateDialog.show()
    }

    const handleDelete = (group: IGroup) => remove(group.id)

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
            <CreateGroupDialog {...createDialog}/>
            <UpdateGroupDialog group={selected} {...updateDialog}/>
        </Container>
    )
}

