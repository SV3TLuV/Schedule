import {LessonForm} from "./LessonForm.tsx";
import {IDialog} from "../../../../../features/models";
import {ILesson} from "../../../../../features/models";
import {useUpdateLessonMutation} from "../../../../../store/apis";
import {IGroup} from "../../../../../features/models";

interface IUpdateLessonDialog extends IDialog {
    lesson: ILesson
    group: IGroup
}

export const UpdateLessonDialog = ({ group, lesson, open, close }: IUpdateLessonDialog) => {
    const [update] = useUpdateLessonMutation()

    const handleSave = (lesson: ILesson) => update(lesson)

    return (
        <LessonForm
            title='Обновление пары'
            show={open}
            lesson={lesson}
            group={group}
            onClose={close}
            onSave={handleSave}
        />
    )
}