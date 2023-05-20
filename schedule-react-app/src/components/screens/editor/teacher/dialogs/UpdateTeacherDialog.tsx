import {IDialog} from "../../../../../features/models/IDialog";
import {ITeacher} from "../../../../../features/models/ITeacher";
import {useUpdateTeacherMutation} from "../../../../../store/apis/teacherApi";
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