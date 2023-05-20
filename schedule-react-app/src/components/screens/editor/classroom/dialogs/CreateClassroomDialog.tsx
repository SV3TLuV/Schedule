import {ClassroomForm} from "./ClassroomForm.tsx";
import {useCreateClassroomMutation} from "../../../../../store/apis/classroomApi.ts";
import {IClassroom} from "../../../../../features/models/IClassroom.ts";
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {emptyClassroom} from "../../../../../utils/defaultModels";

export const CreateClassroomDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateClassroomMutation()

    const handleSave = (classroom: IClassroom) => create(classroom)

    return (
        <ClassroomForm
            title='Добавление кабинета'
            classroom={emptyClassroom}
            show={open}
            onClose={close}
            onSave={handleSave}
        />
    )
}