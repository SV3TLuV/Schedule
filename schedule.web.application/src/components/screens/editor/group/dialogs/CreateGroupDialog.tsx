import {IDialog} from "../../../../../features/models";
import {useCreateGroupMutation} from "../../../../../store/apis";
import {IGroup} from "../../../../../features/models";
import {GroupForm} from "./GroupForm";

export const CreateGroupDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateGroupMutation()

    const handleSave = (group: IGroup) => create(group)

    return (
        <GroupForm
            title='Добавление группы'
            show={open}
            group={{ isAfterEleven: false } as IGroup}
            onClose={close}
            onSave={handleSave}
        />
    )
}