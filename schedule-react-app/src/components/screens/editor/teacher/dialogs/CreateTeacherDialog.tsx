import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateTeacherMutation} from "../../../../../store/apis/teacherApi";
import {ITeacher} from "../../../../../features/models/ITeacher";
import {TeacherForm} from "./TeacherForm";
import {emptyTeacher} from "../../../../../utils/defaultModels";

export const CreateTeacherDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTeacherMutation()

    const handleSave = (teacher: ITeacher) => create(teacher)

    return (
        <TeacherForm
            title='Добавление преподавателя'
            show={open}
            teacher={emptyTeacher}
            onClose={close}
            onSave={handleSave}
        />
    )
}