import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateGroupMutation} from "../../../../../store/apis/groupApi";
import {IGroup} from "../../../../../features/models/IGroup";
import {GroupForm} from "./GroupForm";
import {emptyGroup} from "../../../../../utils/defaultModels";

export const CreateGroupDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateGroupMutation()

    const handleSave = (group: IGroup) => create(group)

    return (
        <GroupForm
            title='Добавление группы'
            show={open}
            group={emptyGroup}
            onClose={close}
            onSave={handleSave}
        />
    )
}