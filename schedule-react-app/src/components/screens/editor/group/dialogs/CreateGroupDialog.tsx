import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateGroupMutation} from "../../../../../store/apis/groupApi";
import {IGroup} from "../../../../../features/models/IGroup";
import {GroupForm} from "./GroupForm";

export const CreateGroupDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateGroupMutation()

    const handleSave = (group: IGroup) => create(group)

    return (
        <GroupForm
            title='Добавление группы'
            show={open}
            group={{} as IGroup}
            onClose={close}
            onSave={handleSave}
        />
    )
}