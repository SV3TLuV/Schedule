import {IDialog} from "../../../../../features/models/IDialog";
import {IGroup} from "../../../../../features/models/IGroup";
import {GroupForm} from "./GroupForm";
import {useUpdateGroupMutation} from "../../../../../store/apis/groupApi";

interface IUpdateGroupDialog extends IDialog {
    group: IGroup
}

export const UpdateGroupDialog = ({ group, open, close }: IUpdateGroupDialog) => {
    const [update] = useUpdateGroupMutation()

    const handleSave = (group: IGroup) => update(group)

    return (
        <GroupForm
            title='Обновление группы'
            show={open}
            group={group}
            onClose={close}
            onSave={handleSave}
        />
    )
}