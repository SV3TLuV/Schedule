import {Container} from "react-bootstrap";
import {DataGridWithPagination} from "../../../ui";
import {EditorToolbar} from "../EditorToolbar.tsx";
import {usePaginationQuery} from "../../../../hooks";
import {useState} from "react";
import {useDialog} from "../../../../hooks";
import {IUser} from "../../../../features/models";
import {useDeleteUserMutation, useGetUsersQuery} from "../../../../store/apis";
import {columns} from "./columns.ts";
import {CreateUserDialog} from "./dialogs";
import {UpdateUserDialog} from "./dialogs";

export const UsersEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery()
    const [selected, setSelected] = useState<IUser>({} as IUser)
    const {data} = useGetUsersQuery(paginationQuery)
    const [remove] = useDeleteUserMutation()
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const handleUpdate = (user: IUser) => {
        setSelected(user)
        updateDialog.show()
    }

    const handleDelete = (user: IUser) => remove(user.id)

    return (
        <Container style={{ height: 'calc(100vh - 72px)', padding: 0 }}>
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
            <CreateUserDialog {...createDialog}/>
            <UpdateUserDialog user={selected} {...updateDialog}/>
        </Container>
    )
}