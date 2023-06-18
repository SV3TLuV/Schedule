import {IDialog} from "../../../../../features/models";
import {useCreateTeacherMutation} from "../../../../../store/apis";
import {ITeacher} from "../../../../../features/models";
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