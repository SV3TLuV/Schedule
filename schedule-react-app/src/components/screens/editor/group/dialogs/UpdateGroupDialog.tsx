import {IDialog} from "../../../../../features/models";
import {IGroup} from "../../../../../features/models";
import {GroupForm} from "./GroupForm";
import {useUpdateGroupMutation} from "../../../../../store/apis";

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