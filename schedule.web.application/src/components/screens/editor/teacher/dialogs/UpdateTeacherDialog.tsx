import {IDialog} from "../../../../../features/models";
import {ITeacher} from "../../../../../features/models";
import {useUpdateTeacherMutation} from "../../../../../store/apis";
import {TeacherForm} from "./TeacherForm";

interface IUpdateTeacherDialog extends IDialog {
    teacher: ITeacher
}

export const UpdateTeacherDialog = ({teacher, open, close}: IUpdateTeacherDialog) => {
    const [update] = useUpdateTeacherMutation()

    const handleSave = (teacher: ITeacher) => update(teacher)

    return (
        <TeacherForm
            title='Обновление преподавателя'
            show={open}
            teacher={teacher}
            onClose={close}
            onSave={handleSave}
        />
    )
}