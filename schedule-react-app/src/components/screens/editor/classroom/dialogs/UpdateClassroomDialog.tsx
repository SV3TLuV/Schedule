import {ClassroomForm} from "./ClassroomForm.tsx";
import {useUpdateClassroomMutation} from "../../../../../store/apis/classroomApi.ts";
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {IClassroom} from "../../../../../features/models/IClassroom.ts";

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