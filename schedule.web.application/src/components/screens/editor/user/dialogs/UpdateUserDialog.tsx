import {useUpdateUserMutation} from "../../../../../store/apis";
import {IUser} from "../../../../../features/models";
import {UserForm} from "./UserForm.tsx";
import {IDialog} from "../../../../../features/models";

interface IUpdateUserDialog extends IDialog {
    user: IUser
}

export const UpdateUserDialog = ({user, open, close}: IUpdateUserDialog) => {
    const [update] = useUpdateUserMutation()

    const handleSave = (user: IUser) => update(user)

    return (
        <UserForm
            title='Обновление пользователя'
            show={open}
            user={user}
            onClose={close}
            onSave={handleSave}
        />
    )
}