import {IDialog} from "../../../../../features/models";
import {useCreateUserMutation} from "../../../../../store/apis";
import {IUser} from "../../../../../features/models";
import {UserForm} from "./UserForm.tsx";

export const CreateUserDialog = ({open, close}: IDialog) => {
    const [create] = useCreateUserMutation()

    const handleSave = (user: IUser) => create(user)

    return (
        <UserForm
            title='Добавление пользователя'
            show={open}
            user={{} as IUser}
            onClose={close}
            onSave={handleSave}
        />
    )
}