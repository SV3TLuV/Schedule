import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateTeacherMutation} from "../../../../../store/apis/teacherApi";
import {ITeacher} from "../../../../../features/models/ITeacher";
import {TeacherForm} from "./TeacherForm";

export const CreateTeacherDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTeacherMutation()

    const handleSave = (teacher: ITeacher) => create(teacher)

    return (
        <TeacherForm
            title='Добавление преподавателя'
            show={open}
            teacher={{} as ITeacher}
            onClose={close}
            onSave={handleSave}
        />
    )
}