import {ClassroomForm} from "./ClassroomForm.tsx";
import {useUpdateClassroomMutation} from "../../../../../store/apis";
import {IDialog} from "../../../../../features/models";
import {IClassroom} from "../../../../../features/models";

interface IUpdateClassroomDialog extends IDialog {
    classroom: IClassroom
}

export const UpdateClassroomDialog = ({classroom, open, close}: IUpdateClassroomDialog) => {
    const [update] = useUpdateClassroomMutation()

    const handleSave = (classroom: IClassroom) => update(classroom)

    return (
        <ClassroomForm
            title='Обновление кабинета'
            classroom={classroom}
            show={open}
            onClose={close}
            onSave={handleSave}
        />
    )
}